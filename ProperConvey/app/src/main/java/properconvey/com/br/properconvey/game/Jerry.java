package properconvey.com.br.properconvey.game;

import android.graphics.Bitmap;

/**
 * Created by root on 20/09/15.
 */
public class Jerry extends Sprite {

    private static final int BMP_ROWS = 5;
    private static final int BMP_COLUMNS = 3;
    private static final int NORMAL_SPEED = 5;

    public Jerry(Bitmap bmp, CanvasView s, int xInicial, int yInicial) {
        super(bmp, s, BMP_ROWS, BMP_COLUMNS, NORMAL_SPEED, xInicial, yInicial);
    }

    @Override
    public void nextFrame() {
        super.nextFrame();

        if (this.getxSpeed() > 0)
            this.setPosOnBitmap(1);
        else if (this.getxSpeed() < 0)
            this.setPosOnBitmap(2);

        if (this.getySpeed() != 0)
            this.setPosOnBitmap(0);
    }

    @Override
    public void stay() {
        super.stay();
        this.setPosOnBitmap(3);
    }

    @Override
    public void handsUp() {
        super.handsUp();
        setPosOnBitmap(4);
    }
}
