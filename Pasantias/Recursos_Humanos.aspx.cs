using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        RH rH;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rH = new RH();
                rH.grid_rh(grid_rh);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}