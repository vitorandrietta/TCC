package game;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;

/**
 * Created by root on 05/09/15.
 */
public class Sprite {

    private static final int BMP_ROWS = 1;
    private static final int BMP_COLUMNS = 3;

    private Bitmap bmp;
    private int frame = 0;
    private int width, height;

    public Sprite (Bitmap bmp) {
        this.bmp = bmp;
        this.width = bmp.getWidth() / BMP_COLUMNS;
        this.height = bmp.getHeight() / BMP_ROWS;
    }

    private void nextFrame() {
        frame = ++frame % BMP_COLUMNS;
    }

    public void onDraw (Canvas canvas) {
        this.nextFrame();

        int sizeX = width * frame;
        int sizeY = 0;

        Rect src = new Rect(sizeX, sizeY, sizeX + width, sizeY + height);
        Rect dst = new Rect(0, 0, width, height);

        canvas.drawBitmap(this.bmp, src, dst, null);
    }

}
