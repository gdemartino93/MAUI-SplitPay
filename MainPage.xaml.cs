using CommunityToolkit.Maui.Alerts;

namespace MAUI_SplitPay
{
    public partial class MainPage : ContentPage
    {

        Dictionary<string, int> tipMap = new Dictionary<string, int>
        {
            { "10", 10 },
            { "15", 15 },
            { "20", 20 }
        };
        decimal totalBill;
        int numberOfPeople = 1;
        int tip;

        public MainPage()
        {
            InitializeComponent();
        }

        private void txtTotal_Completed(object sender, EventArgs e)
        {
            totalBill = decimal.Parse(txtTotal.Text);
            SetCalculatedTip();
        }

        private void sldTip_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            tip = (int)sldTip.Value;
            lblTip.Text = tip.ToString();
            SetCalculatedTip();
        }

        private void ButtonTip_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btn = (Button)sender;

                foreach (string key in tipMap.Keys)
                {
                    if (btn.Text?.Contains(key) == true)
                    {
                        tip = tipMap[key];
                        lblTip.Text = tip.ToString();
                        sldTip.Value = tip;
                        return;
                    }
                }
                
            }
            SetCalculatedTip();
        }

        private async void btnReduceOrAddPerson_Clicked(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btn = (Button)sender;
                switch (btn.Text)
                {
                    case "+":
                        lblCountPeoples.Text = (++numberOfPeople).ToString();
                        SetCalculatedTip();
                        return;
                    case "-":
                        if (numberOfPeople > 1)
                        {
                            lblCountPeoples.Text = (--numberOfPeople).ToString();
                        }
                        else
                        {
                            var toast = Toast.Make("Qualcuno dovrà pagare(?)!!!", CommunityToolkit.Maui.Core.ToastDuration.Short);
                            await toast.Show();
                        }
                        SetCalculatedTip();
                        return;
                }
            }
        }

        private void GetTotalPerPerson()
        {
            decimal total = (tip + totalBill) / numberOfPeople;
            lblTotal.Text = $"€{total:F2}";
        }
        private void SetCalculatedTip()
        {
            decimal tip = (totalBill * this.tip) / 100;
            lblTotalTip.Text = $"€{(tip / numberOfPeople).ToString("F2")}";
            SetSubTotal();
            GetTotalPerPerson();
        }

        private void SetSubTotal()
        {
            lblSubTotal.Text = $"€{(totalBill / numberOfPeople).ToString("F2")}";
        }
    }

}
