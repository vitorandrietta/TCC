package properconvey.com.br.properconvey.faseFloresta;

import android.graphics.Bitmap;
import android.graphics.Canvas;

import java.util.ArrayList;
import java.util.List;

import properconvey.com.br.properconvey.fases.Fase;
import properconvey.com.br.properconvey.fases.FaseBase;
import properconvey.com.br.properconvey.game.CanvasView;
import properconvey.com.br.properconvey.game.Coordenada;
import properconvey.com.br.properconvey.game.Jerry;
import properconvey.com.br.properconvey.game.Sprite;
import properconvey.com.br.properconvey.game.SpriteMove;


/**
 * Created by root on 20/09/15.
 */
public class FaseFlorestaLaranja extends FaseBase implements Fase {

    public FaseFlorestaLaranja(CanvasView screen, Bitmap fundo) {
        super(screen, fundo);

    }

    // o método abaixo implementa a animação de um exercício, movendo todos
    // os sprites conforme suas listas de coordenadas
    @Override
    public void animarExercicio(List<SpriteMove> objects, Canvas canvas) {
        // praticar uma animação com as coordenadas

        this.getBackground().onDraw(canvas);

        for (SpriteMove spm : objects) {
            if (spm.getPontos() == null)
                continue;

            if (spm.getPosAtual() == spm.getPontos().size() ) {
                spm.getSp().stay();
                continue;
            }

            Coordenada c = spm.getPontos().get(spm.getPosAtual());
            spm.getSp().moveToPosition(c);

            if (spm.getSp().isInPostion(c))
                spm.incPosAtual();
        }

    }
}
