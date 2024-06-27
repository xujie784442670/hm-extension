using HmExtension.Standard;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1;

// public class MouseHook
// {
//
//     private Point point;
//
//     private Point Point
//     {
//
//         get { return point; }
//
//         set
//         {
//             if (point != value)
//             {
//                 point = value;
//                 if (MouseMoveEvent != null)
//                 {
//                     var e = new MouseEventArgs(MouseButtons.None, 0, point.X, point.Y, 0);
//                     MouseMoveEvent(this, e);
//                 }
//             }
//         }
//     }
//
//     private int hHook;
//
//     public const int WH_MOUSE_LL = 14;
//
//     public WinApi.HookProc hProc;
//
//     public MouseHook() { this.Point = new Point(); }
//
//     public int SetHook()
//     {
//         hHook = WinApi.SetWindowsHookEx(WH_MOUSE_LL, MouseHookProc, IntPtr.Zero, 0);
//         return hHook;
//     }
//
//     public void UnHook()
//     {
//         WinApi.UnhookWindowsHookEx(hHook);
//     }
//
//     private int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
//     {
//         WinApi.MouseHookStruct MyMouseHookStruct = (WinApi.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(WinApi.MouseHookStruct));
//         if (nCode < 0)
//         {
//             return WinApi.CallNextHookEx(hHook, nCode, wParam, lParam);
//         }
//         else
//         {
//             this.Point = new Point(MyMouseHookStruct.pt.x, MyMouseHookStruct.pt.y);
//             return WinApi.CallNextHookEx(hHook, nCode, wParam, lParam);
//         }
//     }
//
//     //委托+事件（把钩到的消息封装为事件，由调用者处理）
//
//     public delegate void MouseMoveHandler(object sender, MouseEventArgs e);
//
//     public event MouseMoveHandler MouseMoveEvent;
//
// }
