namespace OtheloUI
{
    public static class Validations
    {
        public static bool IsMatrixSizeInputValid(string matrixSizeInput)
        {
            return int.TryParse(matrixSizeInput, out int matrixSize) && (matrixSize == 6 || matrixSize == 8);
        }

        public static bool IsMoveInputValid(string moveInput, string exitKey)
        {
            return moveInput == exitKey || (moveInput.Length == 2 && char.IsLetter(moveInput[0]) && char.IsDigit(moveInput[1]));
        }

        public static bool IsPlayerNameInputValid(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsSecondOponentValid(string secondOponentInput)
        {
            return int.TryParse(secondOponentInput, out int secondOponent) && (secondOponent == 1 || secondOponent == 2);
        }
    }
}
