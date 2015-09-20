package properconvey.com.br.properconvey.fases;

import android.graphics.Bitmap;
import android.graphics.Canvas;

import game.CanvasView;

/**
 * Created by root on 20/09/15.
 */
public class FaseBase {

    private CanvasView screen;
    private Canvas canvas;

    public FaseBase(CanvasView screen, Canvas canvas, Bitmap fundo) {
        this.screen = screen;
        this.canvas = canvas;
        this.screen.setBackground(fundo);
    }

    public CanvasView getScreen() {
        return screen;
    }

    public void setScreen(CanvasView screen) {
        this.screen = screen;
    }

    public Canvas getCanvas() {
        return canvas;
    }

    public void setCanvas(Canvas canvas) {
        this.canvas = canvas;
    }
}
