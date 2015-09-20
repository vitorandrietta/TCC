package properconvey.com.br.properconvey.fases;

import android.graphics.Bitmap;

import java.util.List;

import game.Coordenada;
import game.Sprite;

/**
 * Created by root on 20/09/15.
 */

public interface Fase {
    public abstract void animarExercicio(List<Coordenada> pontos, Sprite jerry);
}
