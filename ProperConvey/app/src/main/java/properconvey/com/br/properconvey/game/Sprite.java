package properconvey.com.br.properconvey.game;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;

/**
 * Created by root on 05/09/15.
 */
public class Sprite {

    private CanvasView screen;

    private Bitmap bmp;
    private int frame = 0;
    private int width, height;

    private int x, y;
    private int xSpeed, ySpeed;
    private int posOnBitmap;

    private int row, col, normalSpeed;

    public Sprite (Bitmap bmp, CanvasView s, int row, int col, int normalSpeed, int xInicial, int yInicial) {
        this.screen = s;
        this.bmp = bmp;

        this.width = bmp.getWidth() / col;
        this.height = bmp.getHeight() / row;

        this.normalSpeed = normalSpeed;
        this.row = row;
        this.col = col;

        this.ySpeed = 0; this.xSpeed = 0;

        this.x = xInicial; this.y = yInicial;

        this.posOnBitmap = 0;
    }

    public void nextFrame() {
        frame = ++frame % this.col;

        this.x += this.xSpeed;
        this.y += this.ySpeed;
    }

    public void moveRight() {
        this.ySpeed = 0;
        this.xSpeed = this.normalSpeed;
    }
    public void moveLeft() {
        this.ySpeed = 0;
        this.xSpeed = -this.normalSpeed;
    }

    public void moveTop() {
        this.ySpeed = -this.normalSpeed;
        this.xSpeed = 0;
    }
    public void moveBottom() {
        this.ySpeed = this.normalSpeed;
        this.xSpeed = 0;
    }

    public void stay() {
        this.ySpeed = 0;
        this.xSpeed = 0;
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
        } else
            this.moveBottom();

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
    public int getxSpeed() {return this.xSpeed;}
    public int getySpeed() {return this.ySpeed;}
    public int getWidth() {return this.width;}
    public int getHeight() {return this.height;}
    public void setPosOnBitmap(int pos) { this.posOnBitmap = pos;}
}
