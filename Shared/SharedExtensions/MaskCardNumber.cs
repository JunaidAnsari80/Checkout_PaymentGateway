namespace SharedExtensions
{
    public static class MaskCardNumber
    {
        /// <summary>
        /// Masked the first 12 digits of card number 
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static string ToMask(this string cardNumber)
        {
            int maskLength = 12;
            string mask = new string('*', maskLength);
            string unMaskEnd = cardNumber.Substring(maskLength, 4);
            return mask + unMaskEnd;
        }
    }
}
