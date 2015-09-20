package properconvey.com.br.properconvey.fases;

import android.graphics.Bitmap;
import android.graphics.Canvas;

import java.util.List;

import properconvey.com.br.properconvey.game.CanvasView;
import properconvey.com.br.properconvey.game.Coordenada;
import properconvey.com.br.properconvey.game.Sprite;


/**
 * Created by root on 20/09/15.
 */
public class FaseFlorestaLaranja extends FaseBase implements Fase {

    public FaseFlorestaLaranja(CanvasView screen,  Bitmap fundo) {
        super(screen, fundo);
    }

    @Override
    public void animarExercicio(List<Coordenada> pontos, Sprite jerry, Canvas canvas) {
        // praticar uma animação com as coordenadas

        this.getBackground().onDraw(canvas);

        if (this.getMove() == pontos.size()-1) {
            jerry.stay();
            return;
        }

        Coordenada c = pontos.get(this.getMove());

        jerry.moveToPosition(c);

        if (jerry.isInPostion(c))
            this.incMove();

        //desenha em canvasView
    }
}
