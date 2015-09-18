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

    private CanvasView screen;

    private Bitmap bmp;
    private int frame = 0;
    private int width, height;

    private int x, y;
    private int xSpeed, ySpeed;

    public Sprite (Bitmap bmp, CanvasView s) {
        this.screen = s;
        this.bmp = bmp;

        this.width = bmp.getWidth() / BMP_COLUMNS;
        this.height = bmp.getHeight() / BMP_ROWS;

        this.xSpeed = 5; this.ySpeed = 5;
        this.x = 10; this.y = 10;
    }

    private void nextFrame() {
        frame = ++frame % BMP_COLUMNS;

        if (this.x + this.width > this.screen.getWidth() || this.x - 5 < 0)
            xSpeed = -xSpeed;

        if (this.y + this.height > this.screen.getHeight() || this.y - 5 < 0)
            ySpeed = -ySpeed;

        this.x += this.xSpeed;
        this.y += this.ySpeed;
    }

    public void onDraw (Canvas canvas) {
        this.nextFrame();

        int sizeX = width * frame;
        int sizeY = 0;

        Rect src = new Rect(sizeX, sizeY, sizeX + width, sizeY + height);
        Rect dst = new Rect(this.x, this.y, width + this.x, height + this.y);

        canvas.drawBitmap(this.bmp, src, dst, null);
    }

}
