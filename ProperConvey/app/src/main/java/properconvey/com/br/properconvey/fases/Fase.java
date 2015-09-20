package properconvey.com.br.properconvey.fases;

import android.graphics.Canvas;

import java.util.List;

import properconvey.com.br.properconvey.game.Coordenada;
import properconvey.com.br.properconvey.game.Sprite;

/**
 * Created by root on 20/09/15.
 */

public interface Fase {
    public abstract void animarExercicio(List<Coordenada> pontos, Sprite jerry, Canvas canvas);
}
