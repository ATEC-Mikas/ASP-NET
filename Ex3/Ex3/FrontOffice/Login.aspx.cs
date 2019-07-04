using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ex3.libs;

namespace Ex3.Pages.FrontOffice
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["error"] = "";
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            User user = Users.FindByUser(txtNickname.Text);
            if (user != null && user.VerifyPassword(txtPassword.Text))
            {
                if(user.Status)
                {
                    Session["User"] = user;
                } else
                {
                    ViewState["info"] = "Pending activation";
                }
            }
            else
                ViewState["error"] = "Invalid User/Password combination";
        }

        protected void linkRegistar_Click(object sender, EventArgs e)
        {
            ViewState["Registar"] = "true";
        }

        protected void btnRegistar_Click(object sender, EventArgs e)
        {
            if(ValidarDadosRegisto())
            {
                User user = new User();
                user.Name = txtRName.Text;
                user.Nickname = txtRNickname.Text;
                user.Password = txtRPassword.Text;
                if(user.save())
                {
                    txtRNickname.Text = "";
                    txtRName.Text = "";
                    txtRPassword.Text = "";
                    txtRConfPassword.Text = "";
                    ViewState["info"] = "Pending activation";
                } else
                {
                    ViewState["error"] = "Erro ao salvar.";
                }

            }
        }

        protected void linkLogin_Click(object sender, EventArgs e)
        {
            ViewState["Registar"] = "";
        }

        private bool validarString(string s)
        {
            return !string.IsNullOrWhiteSpace(s) || !string.IsNullOrEmpty(s);
        }

        private bool ValidarDadosRegisto()
        {
            if (!validarString(txtRConfPassword.Text)
                || !validarString(txtRName.Text)
                || !validarString(txtRNickname.Text)
                || !validarString(txtRPassword.Text))
            {
                ViewState["error"] = "Campos não podem estar vazio";
                return false;                     
            }

            if(Users.FindByUser(txtRNickname.Text)!=null)
            {
                ViewState["error"] = "Nickname already used";
                return false;
            }
            if(txtRPassword.Text!=txtRConfPassword.Text || string.IsNullOrWhiteSpace(txtRPassword.Text) || string.IsNullOrWhiteSpace(txtRConfPassword.Text) )
            {
                ViewState["error"] = "Error with password";
                return false;
            }
            return true;
        }
    }
}