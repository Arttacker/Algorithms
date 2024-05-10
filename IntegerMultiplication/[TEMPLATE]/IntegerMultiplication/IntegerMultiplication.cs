using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>

        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {

            if (X.Length != N)
            {
                int Difference = N - X.Length;
                X=PadRight(X, Difference);
            }
            if (Y.Length != N)
            {
                int Difference = N - Y.Length;
                Y=PadRight(Y, Difference);
            }

            //Base Case : if N = 1
            if (N == 1)
            {
                byte[] multResult = MultiplyReversedNumbers(X, Y);
                return multResult;
            } 
            
            //Divide:
            //1-Split X and Y into two halfs X-->A & B , Y--> C & D
            int halfLength = N / 2;
            // X-->A & B
            byte[] A = new byte[halfLength];
            byte[] B = new byte[halfLength];
            Buffer.BlockCopy(X, 0, A, 0, halfLength);
            Buffer.BlockCopy(X, halfLength, B, 0, halfLength);
            byte[] C = new byte[halfLength];
            byte[] D = new byte[halfLength];
            Buffer.BlockCopy(Y, 0, C, 0, halfLength);
            Buffer.BlockCopy(Y, halfLength, D, 0, halfLength);
            //2-Calculate (A+B) and (C+D)
            byte[] AplusB = AddReversedNumbers(A, B);
            byte[] CplusD = AddReversedNumbers(C, D);
            //Conquer:
            //Recursively multiply 3 subProblems --> [ M1=(AxC) , M2=(BxD) , Z=(A+B)(C+D) ]
            byte[] M1 = IntegerMultiply(A, C, halfLength);
            byte[] M2 = IntegerMultiply(B, D, halfLength);

            byte[] Z = null;
            //***************************************************************************
            //            HANDLING THE ISSUE OF CARRY RETURNED FROM ADDINTION
            //***************************************************************************
            {
                int L1 = AplusB.Length;
                int L2 = CplusD.Length;
                //These two flages for checking the returned arrays are with carry or not
                bool Flag1 = false;
                bool Flag2 = false;
                //---------------------------------------------------------------------------
                //creting an array to store 1x10^L-1 (carry)
                //---------------------------------------------------------------------------
                byte[] CarryAplusB = null;
                byte[] CarryCplusD = null;
                //---------------------------------------------------------------------------
                //creting an array to store the rest numbers of the returned array if there is a carry
                //---------------------------------------------------------------------------
                byte[] RestAplusB = null;
                byte[] RestCplusD = null;
                //---------------------------------------------------------------------------
                //splitting the reurned aray into carry and rest numbers if there is a carry
                //---------------------------------------------------------------------------
                if (L1 > halfLength)
                {
                    //------------------------------------------------------------------------
                    //AplusB RETURNED WITH CARRY
                    //------------------------------------------------------------------------
                    Flag1 = true;

                    //creting an array to store 1x10^L-1 (carry)
                    CarryAplusB = new byte[L1]; //initially stores zeros
                    CarryAplusB[L1 - 1] = 1;

                    //creting an array to store the rest numbers of the returned array if there is a carry
                    RestAplusB = RemoveLastIndex(AplusB);
                }
                if (L2 > halfLength)
                {
                    Flag2 = true;

                    //creting an array to store 1x10^L-1 (carry)
                    CarryCplusD = new byte[L2]; //initially stores zeros
                    CarryCplusD[L2 - 1] = 1;

                    //creting an array to store the rest numbers of the returned array
                    RestCplusD = RemoveLastIndex(CplusD);
                }
                //***************************************************************************
                //                     HANDLING DIFFERENT CASES OF CARRY
                //***************************************************************************
                if (Flag1 && Flag2)
                {
                    //------------------------------------------------------------------------
                    //If both AplusB and CplusD returned with carry
                    //------------------------------------------------------------------------

                    //              c1   r1     c2    r2
                    // Example : ( 100 + 98 )x( 100 + 54 )

                    // 1- mutiply carry_1 by carry_2 --> shift one of them with N/2 zeros
                    byte[] carryXcarry = PadLeft(CarryAplusB, halfLength);

                    // 2- multiply restNumbers by carry --> shift each of them by N/2 zeros
                    byte[] carryXrest = PadLeft(RestCplusD, halfLength);
                    byte[] restXcarry = PadLeft(RestAplusB, halfLength);

                    // 3- multiplying the restNumbers by each others --> Resursion 
                    byte[] restXrest = IntegerMultiply(RestAplusB, RestCplusD, halfLength);

                    //FINAL RESULT FOR Z
                    Z = AddReversedNumbers(AddReversedNumbers(carryXcarry, carryXrest), AddReversedNumbers(restXcarry, restXrest));
                }
                else if (Flag1 && !Flag2)
                {
                    //------------------------------------------------------------------------
                    //If AplusB have returned with carry and CplusD did'not
                    //------------------------------------------------------------------------

                    //              c1   r1     CplusD
                    // Example : ( 100 + 98 ) x ( 98 )

                    // 1- multiply CplusD by carry of AplusB --> shift CplusD with N/2 zeros
                    byte[] CplusDXcarry = PadLeft(CplusD, halfLength);

                    // 2- multiply CplusD by rest of AplusB  --> Resursion
                    byte[] restXCplusD = IntegerMultiply(CplusD, RestAplusB, halfLength);

                    //FINAL RESULT FOR Z
                    Z = AddReversedNumbers(CplusDXcarry, restXCplusD);

                }
                else if (!Flag1 && Flag2)
                {
                    //------------------------------------------------------------------------
                    //If CplusD have returned with carry and AplusB did'not
                    //------------------------------------------------------------------------

                    //           AplusB      c2   r2     
                    // Example : ( 98 ) x ( 100 + 98 )

                    // 1- multiply AplusB by carry of CplusD --> shift AplusB with N/2 zeros
                    byte[] AplusBXcarry = PadLeft(AplusB, halfLength);

                    // 2- multiply AplusB by the rest of CplusD --> Resursion
                    byte[] AplusBXrest = IntegerMultiply(AplusB, RestCplusD, halfLength);

                    //FINAL RESULT FOR Z
                    Z = AddReversedNumbers(AplusBXcarry, AplusBXrest);

                }
                else
                {
                    Z = IntegerMultiply(AplusB, CplusD, halfLength);
                }
            }
            //Combine :
            //1-Subtract M1 and M2 from Z
            byte[] M1plusM2 = AddReversedNumbers(M1, M2);
            byte[] BxCplusAxD = SubtractReversedNumbers(Z, M1plusM2);
            //2-Pad M2 with N zeroes 
            byte[] PaddedM2 = PadLeft(M2, N);
            //3-Pad result of 1 with N/2 zeroes
            byte[] PaddedBxCplusAxD = PadLeft(BxCplusAxD,halfLength);
            //Add 2&3 to M1
            byte[] M2Plus_BxCplusAxD_ = AddReversedNumbers(PaddedM2, PaddedBxCplusAxD);
            byte[] Result = AddReversedNumbers(M1, M2Plus_BxCplusAxD_);
            int neededZeros;

            //checking that the size is 2*N 
            if (Result.Length < (2 * N))
            {
                neededZeros = (2 * N) - Result.Length;
                byte[] finalResult = PadRight(Result, neededZeros);

                return finalResult;
            }
            return Result;
        }

        public static byte[] AddReversedNumbers(byte[] TheFirstArray, byte[] TheSecondArray )
        {
            int TheMaximumLength = Math.Max(TheFirstArray.Length, TheSecondArray.Length);
           
            byte[] TheFinalResult = new byte[TheMaximumLength + 1]; 

            byte TheCarry = 0;
           
            for (int counter = 0; counter < TheMaximumLength; counter++)
            {
                byte TheSummation = (byte)(TheCarry + (counter < TheFirstArray.Length ? TheFirstArray[counter] : (byte)0) + (counter < TheSecondArray.Length ? TheSecondArray[counter] : (byte)0));
                
                TheFinalResult[counter] = (byte)(TheSummation % 10);

                TheCarry = (byte)(TheSummation / 10);
            }


            if (TheCarry > 0)
            {
                TheFinalResult[TheMaximumLength] = TheCarry;

                TheMaximumLength++;
            }


            int resultLength = TheMaximumLength;

            while (resultLength > 1 && TheFinalResult[resultLength - 1] == 0)
            {

                resultLength--;

            }

            byte[] result = new byte[resultLength];


            //Array.Copy(TheFinalResult, result, resultLength);
            Buffer.BlockCopy(TheFinalResult, 0, result, 0, resultLength );


            return result;
        }           
        static byte[] SubtractReversedNumbers(byte[] TheFirstArray, byte[] TheSecondArray)
        {

            int TheMaximumLength = Math.Max(TheFirstArray.Length, TheSecondArray.Length);
            
            byte[] TheFinalResult = new byte[TheMaximumLength];


            int TheSubtraction = 0;

            int TheBorrowed = 0;

           
            int counter = 0;
            while (counter < TheMaximumLength)
            {
                TheSubtraction = TheBorrowed;

                if (counter < TheSecondArray.Length)
                {
                    TheSubtraction -= TheSecondArray[counter];
                }
                if (counter < TheFirstArray.Length)
                {
                    TheSubtraction += TheFirstArray[counter];
                }
                if (TheSubtraction < 0)
                {
                    TheBorrowed = -1;

                    TheSubtraction += 10;
                }
                else
                {
                    TheBorrowed = 0;
                }
                TheFinalResult[counter] = (byte)TheSubtraction;

                counter++;
            }
            return TheFinalResult;

        }
        public static byte[] MultiplyReversedNumbers(byte[] TheFirstArray, byte[] TheSecondArray)
        {
            int TheLengthOfTheFirstArray = TheFirstArray.Length;
            int TheLengthOfTheSecondArray = TheSecondArray.Length;
            int resultLength = TheLengthOfTheFirstArray + TheLengthOfTheSecondArray;
            byte[] TheFinalResult = new byte[resultLength];

            for (int counter1 = 0; counter1 < TheLengthOfTheFirstArray; counter1++)
            {
                byte ThecCarry = 0;
                for (int counter2 = 0; counter2 < TheLengthOfTheSecondArray; counter2++)
                {
                    int product = TheFirstArray[counter1] * TheSecondArray[counter2] + TheFinalResult[counter1 + counter2] + ThecCarry;
                    ThecCarry = (byte)(product / 10);
                    TheFinalResult[counter1 + counter2] = (byte)(product % 10);
                }
                TheFinalResult[counter1 + TheLengthOfTheSecondArray] = ThecCarry;
            }
            return TheFinalResult;
        }
        public static byte[] PadLeft(byte[] TheArray, int TheSize)
        {
            byte[] paddedArr = new byte[TheArray.Length + TheSize];
           
            Buffer.BlockCopy(TheArray, 0, paddedArr, TheSize , TheArray.Length );

            return paddedArr;
        }
        public static byte[] PadRight(byte[] TheArray, int TheSize)
        {

            int newSize = TheArray.Length + TheSize;
            byte[] TheFinalResult = new byte[newSize];
            for (int i = 0; i < TheArray.Length; i++)
            {
                TheFinalResult[i] = TheArray[i];
            }

            return TheFinalResult;

        }
        public static byte[] RemoveLastIndex(byte[] TheOriginalArray)
        {
            byte[] TheFinalResult = new byte[TheOriginalArray.Length - 1];
          
            Buffer.BlockCopy(TheOriginalArray, 0, TheFinalResult, 0, TheOriginalArray.Length - 1 );

            return TheFinalResult;
        }


        #endregion
    }
}
