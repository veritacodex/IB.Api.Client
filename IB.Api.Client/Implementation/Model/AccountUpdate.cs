using System;
using System.Text.Json.Serialization;
using IB.Api.Client.Implementation.Helper;

namespace IB.Api.Client.Implementation.Model
{
    public class AccountUpdate
    {
        [JsonPropertyName("updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonPropertyName("updated")]
        public string Updated
        {
            get
            {
                return $"{UpdatedOn.ToShortDateString()} {UpdatedOn.ToShortTimeString()} ";
            }
        }

        [JsonPropertyName("accountCode")]
        public string AccountCode { get; set; }

        [JsonPropertyName("accountType")]
        public string AccountType { get; set; }

        [JsonPropertyName("availableFunds")]
        public double AvailableFunds { get; set; }

        [JsonPropertyName("buyingPower")]
        public double BuyingPower { get; set; }

        [JsonPropertyName("cashEuro")]
        public double CashEuro { get; set; }

        [JsonPropertyName("cashUsd")]
        public double CashUsd { get; set; }

        [JsonPropertyName("accruedCash")]
        public double AccruedCash { get; set; }

        [JsonPropertyName("baseNetLiquidation")]
        public double BaseNetLiquidation { get; set; }

        [JsonPropertyName("excessLiquidity")]
        public double ExcessLiquidity { get; set; }

        public void SetValue(string key, string value, string currency)
        {
            AccountCode = key == "AccountCode" ? value : AccountCode;
            AccountType = key == "AccountType" ? value : AccountType;
            AvailableFunds = key == "AvailableFunds" ? double.Parse(value) : AvailableFunds;
            BuyingPower = key == "BuyingPower" ? double.Parse(value) : BuyingPower;
            CashEuro = key == "CashBalance" && currency == "EUR" ? double.Parse(value) : CashEuro;
            CashUsd = key == "CashBalance" && currency == "USD" ? double.Parse(value) : CashUsd;
            ExcessLiquidity = key == "ExcessLiquidity" ? double.Parse(value) : ExcessLiquidity;
            AccruedCash = key == "AccruedCash" && currency == "EUR" ? double.Parse(value) : AccruedCash;
            BaseNetLiquidation = key == "NetLiquidationByCurrency" && currency == "BASE" ? double.Parse(value) : BaseNetLiquidation;
        }

        public string GetAsTable()
        {
            var table = new Table("Account", "Code", "Funds", "BuyingPower", "CashBalance", "Liquidation");
            table.AddRow(Updated, AccountCode, AvailableFunds, BuyingPower, CashEuro + "/" + CashUsd, BaseNetLiquidation);
            return table.ToString();
        }
    }
}
