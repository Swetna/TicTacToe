using System;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {   
        #region Private Members

        //Holds the current reusult of cell in activate game
        private MarkType[] results;


        private bool player1Turn;

        private bool gameEnded;


        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// 
        #region Constructor
        public MainWindow(){

            InitilizeComponenet();
            NewGame();
            
        }

        #endregion

        public void NewGame(){
            results = new MarkType[9];

            for (var i=0 ; i < results.Length; i++){
                results[i] = MarkType.Free;

            }

            player1Turn = true;

            // iterate every button on the grid
            Container.Children.Cast<Button >().ToList().ForEach (button => {
                button.Content = string.Empty;
                button.Background = Brushes.Black;
                button.Foreground = Brushes.Blue;
            });
            gameEnded = false;
            
        }

        /// <summary>
        /// Handles a new button clicked by player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e){
            
            if (gameEnded){
                NewGame();
                return;
            }

            var button = (Button) sender;


            // Find the position of the button
            var column = Grid.GetColumn(button);
            var crow = Grid.GetRow(button);

            var index = column + (row * 3);


            if (results[index] != MarkType.Free)
                return; 

            //Based on player the value will be set
            results[index] =  player1Turn ? MarkType.Cross : MarkType.Circle;

            button.Content = player1Turn ? "X" : "O";


            // changing the 0 to green 
            if (!player1Turn)
                button.Foreground = Brushes.Red;
            
                // Bitwise operater to invert value of boolean
            player1Turn ^= true ; 


            // check winner
            checkWinner();

        }

        /// <summary>
        /// If 3 line straigt of same valye then winner
        /// </summary>
        private void checkWinner(){

            // check row 0
            if (results[0] != MarkType.Free && (results[0] & results[1] & results[2] == results[0])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }


            // check row 1
            if (results[3] != MarkType.Free && (results[3] & results[4] & results[5] == results[3])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }


            // check row 2
            if (results[6] != MarkType.Free && (results[6] & results[7] & results[8] == results[6])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }


            // check col 0 
            if (results[0] != MarkType.Free && (results[0] & results[3] & results[6] == results[0])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }



            // check col 1 
            if (results[1] != MarkType.Free && (results[1] & results[4] & results[7] == results[1])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            // check col 2 
            if (results[2] != MarkType.Free && (results[2] & results[5] & results[8] == results[2])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }



            // Diagnoal wins

            if (results[0] != MarkType.Free && (results[0] & results[4] & results[8] == results[0])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            // check col 2 
            if (results[2] != MarkType.Free && (results[2] & results[4] & results[6] == results[2])){

                gameEnded = true;

                // Highlighting the winner blocks
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            //---------------


            // Checking the draw condition 

            if (!results.Any(results => results == MarkType.Free)){
                gameEnded = true;

                Container.Children.Cast<Button >().ToList().ForEach (button => {
                button.Background = Brushes.Orange;
               
            });

            }


        }

    }
}