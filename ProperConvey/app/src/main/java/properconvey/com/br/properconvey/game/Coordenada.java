package properconvey.com.br.properconvey.game;

/**
 * Created by root on 20/09/15.
 */
public class Coordenada {
    private int x, y;

    public Coordenada (int x, int y) {
        this.x = x;
        this.y = y;
    }

    public int getX() {
        return x;
    }

    public void setX(int x) {
        this.x = x;
    }

    public int getY() {
        return y;
    }

    public void setY(int y) {
        this.y = y;
    }
}