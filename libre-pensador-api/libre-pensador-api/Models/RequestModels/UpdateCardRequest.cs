namespace libre_pensador_api.Models.RequestModels
{
    public class UpdateCardRequest
    {
        private string? _cusotmerEmail;
        public string? CustomerEmail 
        {
            get => this._cusotmerEmail;
            set => this._cusotmerEmail = value != null ? value.ToLower() : value;
        }
    }

}
