using System.Drawing;

namespace HZH_Controls.Controls
{
	public class PieItem
	{
		public string Name
		{
			get;
			set;
		}

		public int Value
		{
			get;
			set;
		}
        /// <summary>
        /// Gets or sets the color of the pie.���Ϊ����ʹ��Ĭ����ɫ
        /// </summary>
        /// <value>The color of the pie.</value>
		public Color? PieColor
		{
			get;
			set;
		}
	}
}
