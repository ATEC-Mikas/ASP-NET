using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mota
{
    public partial class Teste : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["erro"] = "";
        }

        protected void buttonOK_Click(object sender, EventArgs e)
        {
            if (pname.Text == "")
            {
                ViewState["erro"] += "\nTem de preencher o primeiro nome";
            }
            if (uname.Text == "")
            {
                ViewState["erro"] += "\nTem de preencher o ultimo nome";
            }
            if(ViewState["erro"] as string=="")
            {
                ViewState["resposta"]=$"Bem vindo {pname.Text} {uname.Text}";
            }
        }
    }
}