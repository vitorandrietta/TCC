package properconvey.com.br.properconvey.game;

import java.util.List;

/**
 * Created by root on 20/09/15.
 */
public class SpriteMove {

    private List<Coordenada> pontos;
    private Sprite sp;
    private int posAtual;

    public SpriteMove(List<Coordenada> pontos, Sprite sp) {
        this.pontos = pontos;
        this.sp = sp;
        posAtual = 0;
    }

    public int getPosAtual() {
        return posAtual;
    }

    public void incPosAtual() {
        this.posAtual++;
    }

    public Sprite getSp() {
        return sp;
    }

    public void setSp(Sprite sp) {
        this.sp = sp;
    }

    public List<Coordenada> getPontos() {
        return pontos;
    }

    public void setPontos(List<Coordenada> pontos) {
        this.pontos = pontos;
    }
}
