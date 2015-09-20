package properconvey.com.br.properconvey.fases;

import android.graphics.Bitmap;
import android.graphics.Canvas;

import java.util.List;

import game.CanvasView;
import game.Coordenada;
import game.Sprite;


/**
 * Created by root on 20/09/15.
 */
public class FaseFlorestaLaranja extends FaseBase implements Fase {

    public FaseFlorestaLaranja(CanvasView screen, Canvas canvas, Bitmap fundo) {
        super(screen, canvas,fundo);
    }

    @Override
    public void animarExercicio(List<Coordenada> pontos, Sprite jerry) {
        // praticar uma animação com as coordenadas

        for (Coordenada c : pontos) {
            jerry.moveBottom();
            //this.getScreen().onDraw(this.getCanvas());

            while (true) {
                if (c.getY() < jerry.getY()) {
                    if (c.getX() < jerry.getX()) {
                        jerry.moveLeft();
                    } else {
                        jerry.moveRight();
                    }

                    break;
                }
            }

            while (true)
                if (c.getX() == jerry.getX())
                    break;

            jerry.stay();
        }

        //desenha em canvasView
    }
}
