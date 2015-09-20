package properconvey.com.br.properconvey.game;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;

/**
 * Created by root on 05/09/15.
 */
public class Sprite {

    private static final int BMP_ROWS = 5;
    private static final int BMP_COLUMNS = 3;
    private static final int NORMAL_SPEED = 5;

    private CanvasView screen;

    private Bitmap bmp;
    private int frame = 0;
    private int width, height;

    private int x, y;
    private int xSpeed, ySpeed;
    private int posOnBitmap;


    public Sprite (Bitmap bmp, CanvasView s) {
        this.screen = s;
        this.bmp = bmp;

        this.width = bmp.getWidth() / BMP_COLUMNS;
        this.height = bmp.getHeight() / BMP_ROWS;

        this.ySpeed = NORMAL_SPEED; this.xSpeed = 0;
        this.x = 10; this.y = 10;
        this.posOnBitmap = 0;
    }

    private void nextFrame() {
        frame = ++frame % BMP_COLUMNS;

        //não sair da tela, código
        //if (this.x + this.width > this.screen.getWidth() || this.x - 5 < 0)
        //    xSpeed = -xSpeed;
        //if (this.y + this.height > this.screen.getHeight() || this.y - 5 < 0)
        //    ySpeed = -ySpeed;

        if (this.xSpeed > 0)
            this.posOnBitmap = 1;
        else if (this.xSpeed < 0)
            this.posOnBitmap = 2;

        if (this.ySpeed != 0)
            this.posOnBitmap = 0;

        this.x += this.xSpeed;
        this.y += this.ySpeed;
    }

    public void moveRight() {
        this.ySpeed = 0;
        this.xSpeed = NORMAL_SPEED;
    }
    public void moveLeft() {
        this.ySpeed = 0;
        this.xSpeed = -NORMAL_SPEED;
    }

    public void moveTop() {
        this.ySpeed = -NORMAL_SPEED;
        this.xSpeed = 0;
    }
    public void moveBottom() {
        this.ySpeed = NORMAL_SPEED;
        this.xSpeed = 0;
    }

    public void stay() {
        this.ySpeed = 0;
        this.xSpeed = 0;
        this.posOnBitmap = 3;
    }

    public boolean isInPostion(Coordenada ponto) {
        return (this.x >= ponto.getX() && this.y >= ponto.getY());
    }

    public void moveToPosition(Coordenada ponto) {
        if (this.y >= ponto.getY()) {
            if (this.x < ponto.getX()) {
                this.moveRight();
            } else {
                this.moveLeft();
            }
        }

        if (this.x >= ponto.getX())
            this.stay();
    }

    public void onDraw (Canvas canvas) {
        this.nextFrame();

        int sizeX = width * frame;
        int sizeY = posOnBitmap * this.height;

        Rect src = new Rect(sizeX, sizeY, sizeX + width, sizeY + height);
        Rect dst = new Rect(this.x, this.y, width + this.x, height + this.y);

        canvas.drawBitmap(this.bmp, src, dst, null);
    }

    public int getX() {return this.x;}
    public int getY() {return this.y;}
    public int getWidth() {return this.width;}
    public int getHeight() {return this.height;}
}
