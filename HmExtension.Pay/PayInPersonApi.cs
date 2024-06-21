using System;
using System.Collections.Generic;
using System.Linq;
using Aop.Api.Domain;
using Aop.Api.Request;
using System.Threading.Tasks;
using Aop.Api.Response;

namespace HmExtension.Pay;

/// <summary>
/// 支付宝当面付API服务
/// </summary>
public class PayInPersonApi
{
    private static readonly Dictionary<string, string> _payScene = new Dictionary<string, string>
    {
        { "BAR_CODE", "bar_code" },
        { "SECURITY_CODE", "security_code" }
    };

    // public const string BAR_CODE = "bar_code";
    // public const string SECURITY_CODE = "security_code";
    /// <summary>
    /// 支付场景
    /// </summary>
    public enum PayScene
    {
        /// <summary>
        /// 当面付条码支付场景；
        /// </summary>
        BAR_CODE,

        /// <summary>
        /// 当面付刷脸支付场景，对应的auth_code为fp开头的刷脸标识串；
        /// </summary>
        SECURITY_CODE
    }

    /// <summary>
    /// <a href="https://opendocs.alipay.com/open/1f1fe18c_alipay.trade.pay?pathHash=29c9a9ba&amp;ref=api&amp;scene=32">alipay.trade.pay(统一收单交易支付接口)</a>
    /// <example>
    /// <code>
    /// // 创建当面付API实例
    /// PayInPersonApi payInPersonApi = new PayInPersonApi();
    /// // 创建订单号
    /// var outTradeNo = Guid.NewGuid().ToString();
    /// // 调用支付接口
    /// var task = payInPersonApi.Pay(outTradeNo,"测试",100,"285288625127295717");
    /// task.ContinueWith(t =&gt;
    /// {
    ///     // 输出支付结果
    ///     if(t.Result.IsSuccess()){
    ///         Console.WriteLine("支付成功");
    ///     }else{
    ///         Console.WriteLine("支付失败: "+t.Exception.StackTrace);
    ///     }
    /// });
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="outTradeNo">* 商户订单号</param>
    /// <param name="subject">* 订单标题</param>
    /// <param name="totalAmount">* 订单总金额</param>
    /// <param name="authCode">* 支付授权码</param>
    /// <param name="payScene">支付场景</param>
    /// <param name="productCode">产品码</param>
    /// <param name="sellerId">卖家支付宝用户ID</param>
    /// <param name="storeId">商户门店编号</param>
    /// <param name="operatorId">商户操作员编号</param>
    /// <param name="terminalId">商户机具终端编号</param>
    /// <param name="queryOptions">返回参数选项</param>
    /// <param name="extendParams">业务扩展参数</param>
    /// <param name="businessParams">商户传入业务信息，具体值要和支付宝约定，应用于安全，营销等参数直传场景</param>
    /// <param name="promoParams">优惠明细参数，通过此属性补充营销参数。 注：仅与支付宝协商后可用。</param>
    /// <param name="goodsDetails">订单包含的商品列表信息</param>
    /// <returns>异步任务</returns>
    public Task<AlipayTradePayResponse> Pay(
        string outTradeNo,
        string subject,
        double totalAmount,
        string authCode,
        PayScene payScene = PayScene.BAR_CODE,
        string productCode = default,
        string sellerId = default,
        string storeId = default,
        string operatorId = default,
        string terminalId = default,
        List<string> queryOptions = default,
        ExtendParams extendParams = default,
        BusinessParams businessParams = default,
        PromoParam promoParams = default,
        params GoodsDetail[] goodsDetails)
    {
        AlipayTradePayRequest request = new AlipayTradePayRequest();
        AlipayTradePayModel model = new AlipayTradePayModel();
        model.Subject = subject;
        model.OutTradeNo = outTradeNo;
        model.TotalAmount = totalAmount.ToString("F2");
        model.AuthCode = authCode;
        model.Scene = _payScene[Enum.GetName(typeof(PayScene), payScene) ?? "bar_code"];
        model.ProductCode = productCode;
        model.SellerId = sellerId;
        model.StoreId = storeId;
        model.OperatorId = operatorId;
        model.TerminalId = terminalId;
        model.QueryOptions = queryOptions;
        model.ExtendParams = extendParams;
        model.BusinessParams = businessParams;
        model.PromoParams = promoParams;
        model.GoodsDetail = goodsDetails.ToList();
        request.SetBizModel(model);
        // 创建异步任务
        var taskSource = new TaskCompletionSource<AlipayTradePayResponse>();
        // 执行异步任务
        Task.Run(() =>
        {
            try
            {
                // 调用支付宝接口
                var response = AlipayContext.Execute(request);
                if (response.IsSuccess())
                {
                    taskSource.SetResult(response);
                    return;
                }
                AlipayContext.Execption(response);
            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        });
        return taskSource.Task;
    }

    /// <summary>
    /// <a href="https://opendocs.alipay.com/open/6f534d7f_alipay.trade.query?pathHash=98c03720&amp;ref=api&amp;scene=23">alipay.trade.query(统一收单交易查询)</a>
    /// <example>
    /// <code>
    /// // 创建当面付API实例
    /// PayInPersonApi payInPersonApi = new PayInPersonApi();
    /// // 调用查询接口
    /// var task = payInPersonApi.Query("bb1eee5a-6cc6-4040-b5f9-b0f49dbfcec3");
    /// task.ContinueWith(t =&gt;
    /// {
    ///     // 打印订单状态
    ///     t.Result.TradeStatus.Println("TradeStatus: ");
    /// });
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="outTradeNo">商户订单号</param>
    /// <param name="tradeNo">支付宝交易号</param>
    /// <param name="queryOptions">查询选项，商户通过上送该参数来定制同步需要额外返回的信息字段</param>
    /// <returns>异步任务</returns>
    /// <exception cref="ArgumentException"></exception>
    public Task<AlipayTradeQueryResponse> Query(
        string outTradeNo = default,
        string tradeNo = default,
        params string[] queryOptions)
    {
        if (string.IsNullOrWhiteSpace(outTradeNo) && string.IsNullOrWhiteSpace(tradeNo))
            throw new ArgumentException("商户订单号和支付宝交易号不能同时为空");
        var taskSource = new TaskCompletionSource<AlipayTradeQueryResponse>();
        AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
        AlipayTradeQueryModel model = new AlipayTradeQueryModel
        {
            OutTradeNo = outTradeNo,
            TradeNo = tradeNo,
            QueryOptions = queryOptions.ToList()
        };
        request.SetBizModel(model);
        Task.Run(() =>
        {
            try
            {
                var response = AlipayContext.Execute(request);
                if (response.IsSuccess())
                {
                    taskSource.SetResult(response);
                    return;
                }
                AlipayContext.Execption(response);

            }
            catch (Exception ex)
            {
                taskSource.SetException(ex);
            }
        });
        return taskSource.Task;
    }
    /// <summary>
    /// <a href="https://opendocs.alipay.com/open/3aea9b48_alipay.trade.refund?pathHash=04122275&amp;ref=api&amp;scene=common">alipay.trade.refund(统一收单交易退款接口)</a>
    /// <example>
    /// <code>
    /// // 创建当面付API实例
    /// PayInPersonApi payInPersonApi = new PayInPersonApi();
    /// // 执行退款API
    /// var task = payInPersonApi.Refund(100, "bb1eee5a-6cc6-4040-b5f9-b0f49dbfcec3");
    /// task.ContinueWith(t =&gt;
    /// {
    ///     // 打印退款金额
    ///     if (t.Result.IsSuccess())
    ///     {
    ///         t.Result.RefundFee.Println("成功退款: ");
    ///     }
    ///     AlipayContext.Execption(t.Result);
    /// });
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="refundAmount">退款金额</param>
    /// <param name="outTradeNo">商户订单号</param>
    /// <param name="tradeNo">支付宝交易号</param>
    /// <param name="refundReason">退款原因说明</param>
    /// <param name="outRequestNo">退款请求号</param>
    /// <param name="relatedSettleConfirmNo">针对账期交易，在确认结算后退款的话，需要指定确认结算时的结算单号</param>
    /// <param name="queryOptions">查询选项。 商户通过上送该参数来定制同步需要额外返回的信息字段</param>
    /// <param name="refundRoyaltyParameters">退分账明细信息</param>
    /// <param name="refundGoodsDetails">退款包含的商品列表信息</param>
    /// <returns>异步任务</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<AlipayTradeRefundResponse> Refund(
        double refundAmount,
        string outTradeNo = default,
        string tradeNo = default,
        string refundReason = default,
        string outRequestNo = default,
        string relatedSettleConfirmNo = default,
        List<string> queryOptions = default,
        List<OpenApiRoyaltyDetailInfoPojo> refundRoyaltyParameters = default,
        params RefundGoodsDetail[] refundGoodsDetails)
    {
        if(string.IsNullOrWhiteSpace(outRequestNo)&&string.IsNullOrWhiteSpace(outTradeNo)&&string.IsNullOrWhiteSpace(tradeNo))
            throw new ArgumentException("商户订单号、支付宝交易号和退款请求号不能同时为空");

        var taskSource = new TaskCompletionSource<AlipayTradeRefundResponse>();

        AlipayTradeRefundRequest request = new AlipayTradeRefundRequest();
        AlipayTradeRefundModel model = new AlipayTradeRefundModel
        {
            RefundAmount = refundAmount.ToString("F2"),
            OutTradeNo = outTradeNo,
            TradeNo = tradeNo,
            RefundReason = refundReason,
            OutRequestNo = outRequestNo,
            RelatedSettleConfirmNo = relatedSettleConfirmNo,
            QueryOptions = queryOptions,
            RefundRoyaltyParameters = refundRoyaltyParameters,
            RefundGoodsDetail = refundGoodsDetails.ToList()
        };

        Task.Run(() =>
        {
            try
            {
                request.SetBizModel(model);
                var response = AlipayContext.Execute(request);
                if (response.IsSuccess())
                {
                    taskSource.SetResult(response);
                    return;
                }
                AlipayContext.Execption(response);
            }
            catch (Exception a)
            {
                taskSource.SetException(a);
            }
        });
        return taskSource.Task;
    }

    /// <summary>
    /// <a href="https://opendocs.alipay.com/open/f540afd8_alipay.trade.precreate?pathHash=d3c84596&amp;ref=api&amp;scene=19">alipay.trade.precreate(统一收单线下交易预创建)</a>
    /// <example>
    /// <code>
    /// // 创建当面付API实例
    /// PayInPersonApi payInPersonApi = new PayInPersonApi();
    /// // 生成订单号
    /// var outTradeNo = Guid.NewGuid().ToString();
    /// // 执行API
    /// var task = payInPersonApi.Precreate(outTradeNo,"预创建订单",1000);
    /// task.ContinueWith(t =&gt;
    /// {
    ///     // 获取返回的二维码内容
    ///     if (t.Result.IsSuccess())
    ///     {
    ///         // 得到二维码内容
    ///         var qrCode = t.Result.QrCode;
    ///         // 将二维码内容转换为二维码图片并保存
    ///         qrCode.ToQRCode().Save("test.png");
    ///     }
    /// });
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="outTradeNo">商户订单号</param>
    /// <param name="subject">订单标题</param>
    /// <param name="totalAmount">订单总金额，单位为元，精确到小数点后两位，取值范围为 [0.01,100000000]，金额不能为 0</param>
    /// <param name="discountableAmount">可打折金额。</param>
    /// <param name="productCode">销售产品码</param>
    /// <param name="sellerId">卖家支付宝用户 ID</param>
    /// <param name="storeId">商户门店编号</param>
    /// <param name="operatorId">商户操作员编号</param>
    /// <param name="terminalId">商户机具终端编号</param>
    /// <param name="merchantOrderNo">商户原始订单号，最大长度限制 32 位</param>
    /// <param name="body">订单附加信息</param>
    /// <param name="extendParams">业务扩展参数</param>
    /// <param name="businessParams">商户传入业务信息，具体值要和支付宝约定，应用于安全，营销等参数直传场景</param>
    /// <param name="goodsDetails">订单包含的商品列表信息</param>
    /// <returns>异步任务</returns>
    public Task<AlipayTradePrecreateResponse> Precreate(
        string outTradeNo,
        string subject,
        double totalAmount,
        double discountableAmount=default,
        string productCode= "FACE_TO_FACE_PAYMENT",
        string sellerId = default,
        string storeId = default,
        string operatorId = default,
        string terminalId = default,
        string merchantOrderNo = default,
        string body = default,
        ExtendParams extendParams=default,
        BusinessParams businessParams=default,
        params GoodsDetail[] goodsDetails
        )
    {

        var ts = new TaskCompletionSource<AlipayTradePrecreateResponse>();

        AlipayTradePrecreateRequest request = new AlipayTradePrecreateRequest();
        AlipayTradePrecreateModel model = new AlipayTradePrecreateModel
        {
            OutTradeNo = outTradeNo,
            Subject = subject,
            TotalAmount = totalAmount.ToString("F2"),
            DiscountableAmount = discountableAmount.ToString("F2"),
            ProductCode = productCode,
            SellerId = sellerId,
            StoreId = storeId,
            OperatorId = operatorId,
            TerminalId = terminalId,
            MerchantOrderNo = merchantOrderNo,
            Body = body,
            ExtendParams = extendParams,
            BusinessParams = businessParams,
            GoodsDetail = goodsDetails.ToList()
        };
        request.SetBizModel(model);
        Task.Run(() =>
        {
            try
            {
                var response = AlipayContext.Execute(request);
                if (response.IsSuccess())
                {
                    ts.SetResult(response);
                    return;
                }
                AlipayContext.Execption(response);
            }
            catch (Exception e)
            {
                ts.SetException(e);
            }
        });
        return ts.Task;

    }
}