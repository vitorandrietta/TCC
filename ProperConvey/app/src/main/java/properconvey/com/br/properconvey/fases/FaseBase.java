package properconvey.com.br.properconvey.fases;

import android.graphics.Bitmap;

import properconvey.com.br.properconvey.game.Background;
import properconvey.com.br.properconvey.game.CanvasView;

/**
 * Created by root on 20/09/15.
 */
public class FaseBase {

    private CanvasView screen;
    private Background background;
    private int move;

    public FaseBase(CanvasView screen, Bitmap fundo) {
        this.screen = screen;
        this.background = new Background(fundo, screen);
        this.move= 0;
    }

    public CanvasView getScreen() {
        return screen;
    }

    public void setScreen(CanvasView screen) {
        this.screen = screen;
    }

    public Background getBackground() {
        return background;
    }

    public void setBackground(Background background) {
        this.background = background;
    }

    public int getMove() {
        return move;
    }

    public void incMove() {
        this.move++;
    }
}
