using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace UserControlLibrary
{
    [DefaultEvent("ClipboardChanged")]
    public partial class ClipboardMonitor : Control
    {
        public ClipboardMonitor()
        {
            this.BackColor = Color.Red;
            this.Visible = false;

            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }

        IntPtr nextClipboardViewer;

        /// <summary>
        /// Clipboard contents changed.
        /// </summary>
        public event OnClipboardChanged ClipboardChanged;
        public delegate void OnClipboardChanged(object sender, EventClipBoardArg e);
        private void _ClipboardChanged(EventClipBoardArg e)
        {
            if (ClipboardChanged == null) return;
            ClipboardChanged(this, e);
        }

        protected override void Dispose(bool disposing)
        {
            ChangeClipboardChain(this.Handle, nextClipboardViewer);
        }

        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetClipboardOwner();

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    IntPtr clipOwn = GetClipboardOwner();
                    uint pid = 0;
                    GetWindowThreadProcessId(clipOwn, out pid);
                    System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById((int)pid);
                    _ClipboardChanged(new EventClipBoardArg { ClipboardOwnerName = p.ProcessName });
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }

    public class EventClipBoardArg : EventArgs
    {
        public string ClipboardOwnerName { get; set; }
    }
}
