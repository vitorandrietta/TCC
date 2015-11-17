package properconvey.com.br.properconvey.game;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Rect;

/**
 * Created by root on 19/09/15.
 */
public class Background {

    private Bitmap bmp;
    private CanvasView screen;

    private int width, height;

    public Background(Bitmap bmp,  CanvasView canvasView) {
        this.bmp = bmp;
        this.screen = canvasView;

        this.width = bmp.getWidth();
        this.height = bmp.getHeight();
    }

    public void onDraw(Canvas canvas) {
        //this.moveBackground();

        Rect src = new Rect(0, 0, width, height);
        Rect dst = new Rect(0, 0, this.screen.getWidth(), this.screen.getHeight());

        canvas.drawBitmap(this.bmp,src, dst, null);
    }

}
