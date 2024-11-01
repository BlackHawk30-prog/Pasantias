using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Oficial oficial;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oficial = new Oficial();
                oficial.grid_oficial(GridView1);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
 


        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btn_rech_Click(object sender, EventArgs e)
        {

        }
    }
}