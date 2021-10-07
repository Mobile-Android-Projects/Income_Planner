using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@mipmap/ic_incomeIcon")]
    public class MainActivity : AppCompatActivity
    {
        EditText dollarsPaHour;
        EditText hoursPaDay;
        EditText percentTaxRate;
        EditText percentSavingsRate;

        Button calculateBtn;

        TextView workSummary;
        TextView grossIncome;
        TextView taxPayable;
        TextView annualSavings;
        TextView spendableIncome;

        RelativeLayout resultLayout;

        bool inputCalculated = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Hook up Widgets
            ConnectViews();
            calculateBtn.Click += CalculateBtn_Click;
                
        }

        private void CalculateBtn_Click(object sender, System.EventArgs e)
        {

            if(inputCalculated)
            {
                inputCalculated = false;
                calculateBtn.Text = "Calculate";
                ClearViews();
                return;
            }

            //Get input from the user
            double hourlyDollars = double.Parse(dollarsPaHour.Text);
            double dailyHours = double.Parse(hoursPaDay.Text);
            double taxRate = double.Parse(percentTaxRate.Text);
            double savingsRate = double.Parse(percentSavingsRate.Text);

            //Calculate results
            double yearlyHours = dailyHours * 5 * 50; //assume 5 day work week,two weeks off pa year
            double grossIncomeEarned = yearlyHours * hourlyDollars;
            double grossAnnualTax = ((taxRate / 100) * grossIncomeEarned);
            double grossAnnualSavings = ((savingsRate / 100) * grossIncomeEarned);
            double annualSpendableIncome = grossIncomeEarned - (grossAnnualSavings + grossAnnualTax);

            //Display results to user
            workSummary.Text = yearlyHours.ToString("#,##") + " HRS";
            grossIncome.Text = grossIncomeEarned.ToString("#,##") + " USD";
            taxPayable.Text = grossAnnualTax.ToString("#,##") + " USD";
            annualSavings.Text = grossAnnualSavings.ToString("#,##") + " USD";
            spendableIncome.Text = annualSpendableIncome.ToString("#,##") + " USD";

            resultLayout.Visibility = Android.Views.ViewStates.Visible;
            inputCalculated = true;
            calculateBtn.Text = "Clear";
        }

        private void ConnectViews()
        {
            //Edit Texts Bindings
            dollarsPaHour = FindViewById<EditText>(Resource.Id.incomePerHourEDTX);
            hoursPaDay = FindViewById<EditText>(Resource.Id.workHoursEDTX);
            percentTaxRate = FindViewById<EditText>(Resource.Id.taxRateEDTX);
            percentSavingsRate = FindViewById<EditText>(Resource.Id.savingsRateEDTX);

            //Button Bindings
            calculateBtn = FindViewById<Button>(Resource.Id.calculateBtn);

            //Text View Bindings
            workSummary = FindViewById<TextView>(Resource.Id.annualWorkHoursDTXT);
            grossIncome = FindViewById<TextView>(Resource.Id.annualGrossIncomeDTXT);
            taxPayable = FindViewById<TextView>(Resource.Id.annualTaxDTXT);
            annualSavings = FindViewById<TextView>(Resource.Id.annualSavingsDTXT);
            spendableIncome = FindViewById<TextView>(Resource.Id.spendableIncomeDTXT);

            //Relative Layout Binding
            resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultLayout);
        }
     
        private void ClearViews()
        {
            dollarsPaHour.Text = " ";
            hoursPaDay.Text = " ";
            percentTaxRate.Text = " ";
            percentSavingsRate.Text = " ";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }
    }
}