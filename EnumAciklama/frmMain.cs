using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnumAciklama
{

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public static string GetEnumAciklama(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);


            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T bir System.Enum olmalıdır");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var data = EnumToList<Egitim>();
            var comboSource = new List<EnumResult>();
            foreach (var item in data)
            {
                comboSource.Add(new EnumResult
                {
                    Id = (int)item,
                    Deger = GetEnumAciklama(item)
                });
            }

            comboBox1.DataSource = comboSource;
            comboBox1.DisplayMember = "Deger";
            comboBox1.ValueMember = "Id";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEnumDeger.Text = (comboBox1.SelectedItem as EnumResult).Id.ToString();
        }
    }
    enum Egitim
    {
        [Description("İlk Okul Mezunu")]
        IlkOkul,
        [Description("Üniversite Mezunu")]
        Lisans,
        [Description("Yüksek Lisans Mezunu")]
        YuksekLisans
    }

    class EnumResult
    {
        public int Id { get; set; }
        public string Deger { get; set; }
    }
}
