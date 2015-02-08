using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace SmileWei.EmbeddedApp
{
    public class AppFilenameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
            //return base.GetEditStyle(context);
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService wfes = provider.GetService(
                typeof(IWindowsFormsEditorService)) as
                IWindowsFormsEditorService;

            if (wfes != null)
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.Filter = "可执行程序 (*.exe)|*.exe";
                fileDlg.Multiselect = false;
                if (fileDlg.ShowDialog() == DialogResult.OK)
                {
                    return fileDlg.FileName;
                }
                else
                {
                    return string.Empty;
                }
            }
            return value;
            //return base.EditValue(context, provider, value);
        }
    }
}
