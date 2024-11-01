using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class VistaHJ : System.Web.UI.Page
    {
        Vista_HJ vistahj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                vistahj = new Vista_HJ();
                vistahj.grid_hojas(grid_hoja);
            }
        }

        protected void grid_hojas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}