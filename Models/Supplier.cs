namespace Product_Supplier_Registration.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string QRCode { get; set; }

        public string GenerateQRCode()
        {
            QRCode = "";
            return QRCode+$"{CNPJ} - {ZipCode}";
        }
    }
}
