package game;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;

/**
 * Created by root on 05/09/15.
 */
public class Sprite {

    private static final int BMP_ROWS = 4;
    private static final int BMP_COLUMNS = 3;

    private Bitmap bmp;
    private int velocity;

    private int x, y;
    private int frame;
    private int width, height;

    public Sprite(Bitmap bmp) {
        this.bmp = bmp;
        this.frame = 0;
        this.width = bmp.getWidth() / BMP_ROWS;
        this.height = bmp.getHeight() / BMP_COLUMNS;

        this.x = 10; this.y = 10;
    }

    private void nextFrame() {
        frame = ++frame % BMP_ROWS;
    }

    public void onDraw (Canvas canvas) {
        this.nextFrame();

        int sizeX = width * frame;
        int sizeY = 1 * height;

        Rect src = new Rect(sizeX, sizeY, sizeX+ width, sizeY + height);
        Rect dst = new Rect(x, y, x + width, y + height);

        canvas.drawBitmap(this.bmp, src, dst, null);
        canvas.save();
    }

}
