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
            return QRCode.Replace("%CNPJ%", CNPJ)
                         .Replace("%CEP%", ZipCode)
                         .Replace("%DATACADASTRO%", CreatedAt.ToString("dd/MM/yyyy"));
        }
    }
}
