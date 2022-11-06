public class Judge : MonoBehaviour {
    public int CheckGameOver(CoastController rightCoastCtrl, CoastController leftCoastCtrl, BoatController boatCtrl) {
        int rightPriest = 0;
        int rightDevil = 0;
        int leftPriest = 0;
        int leftDevil = 0;
        int status = 0;

        rightPriest += rightCoastCtrl.GetCharacterNum()[0];
        rightDevil += rightCoastCtrl.GetCharacterNum()[1];
        leftPriest += leftCoastCtrl.GetCharacterNum()[0];
        leftDevil += leftCoastCtrl.GetCharacterNum()[1];

        // Win
        if (leftPriest + leftDevil == 6) {
            status = 2; 
        }
        
        if (boatCtrl.boat.Location == Location.right) {
            rightPriest += boatCtrl.GetCharacterNum()[0];
            rightDevil += boatCtrl.GetCharacterNum()[1];
        } else {
            leftPriest += boatCtrl.GetCharacterNum()[0];
            leftDevil += boatCtrl.GetCharacterNum()[1];
        }

        // Lose
        if ((rightPriest < rightDevil && rightPriest > 0) ||
            (leftPriest < leftDevil && leftPriest > 0)) {
            status = 1;
        }

        return status;
    }
}