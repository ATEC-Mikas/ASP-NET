using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Exercicio_RSS
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ListItem[] Source = 
                {
                    new ListItem("" , "Default"),
                    new ListItem("Ultimas" , "http://feeds.jn.pt/JN-Ultimas"),
                    new ListItem("Justica", "http://feeds.jn.pt/JN-Justica"),
                    new ListItem("Gente", "http://feeds.jn.pt/JN-Gente"),
                    new ListItem("Nacional", "http://feeds.jn.pt/JN-Nacional"),
                    new ListItem("Mundo", "http://feeds.jn.pt/JN-Mundo"),
                    new ListItem("Economia", "http://feeds.jn.pt/JN-Economia"),
                    new ListItem("Desporto", "http://feeds.jn.pt/JN-Desporto")
                };
                dropdownListaRSS.DataSource = Source;
                dropdownListaRSS.DataBind();
            }
        }

        protected void dropdownListaRSS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] r = dropdownListaRSS.SelectedItem.Value.Split('/');
            Response.Redirect($"~/{r[r.Length-1]}.aspx");
        }
    }
}