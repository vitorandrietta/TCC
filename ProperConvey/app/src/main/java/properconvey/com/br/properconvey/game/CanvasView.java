package properconvey.com.br.properconvey.game;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

import java.util.ArrayList;
import java.util.List;

import properconvey.com.br.properconvey.R;
import properconvey.com.br.properconvey.fases.Fase;
import properconvey.com.br.properconvey.faseFloresta.FaseFlorestaLaranja;

/**
 * Created by root on 05/09/15.
 */
public class CanvasView extends SurfaceView {

    private Context context;
    private Jerry jerry;
    private Sprite laranja;
    private Jerry jk;
    public Sprite getLaranja() {
        return laranja;
    }

    public void setLaranja(Sprite laranja) {
        this.laranja = laranja;
    }

    public Jerry getJerry() {
        return jerry;
    }

    public void setJerry(Jerry jerry) {
        this.jerry = jerry;
    }

    // contém o canvas da surfaceView
    private SurfaceHolder holder;

    //game Thread é o objeto que controla a thread de UI
    private GameLoopThread gameThread;

    private List<Coordenada> c;
    private List<SpriteMove> spm;

    // variável de controle da interface de fase atual, posteriomente
    // a ser passada por parâmetro no construtor
    private Fase ffl;

    public CanvasView(Context context) {
        super(context);
        this.context = context;

        this.gameThread = new GameLoopThread(this);
        this.holder = getHolder();
        this.holder.addCallback(new SurfaceHolder.Callback() {
            @Override
            public void surfaceCreated(SurfaceHolder holder) {
                gameThread.setRunning(true);
                gameThread.start();
            }

            @Override
            public void surfaceDestroyed(SurfaceHolder holder) {
                boolean retry = true;
                gameThread.setRunning(false);
                while (retry) {
                    try {
                        gameThread.join();
                        retry = false;
                    } catch (InterruptedException e) {
                    }
                }
            }

            @Override
            public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {
            }
        });


        Bitmap bmp = BitmapFactory.decodeResource(getResources(), R.drawable.outrojerry);
        this.jerry = new Jerry(bmp,this, 10, 10);

        this.c = new ArrayList<Coordenada>();
        this.c.add(new Coordenada(100,100));
        this.spm = new ArrayList<SpriteMove>();
        this.spm.add(new SpriteMove(this.c, jerry));


        bmp = BitmapFactory.decodeResource(getResources(), R.drawable.orange);
        this.laranja = new Sprite(bmp, this, 1, 4, 5, 350, 420);
        this.spm.add(new SpriteMove(null,laranja) );

        // para testes na faseFloresta, mudar posteriormente
        this.ffl = new FaseFlorestaLaranja(this, BitmapFactory.decodeResource(getResources(), R.drawable.floresta));

    }

    public void animarParteFase(Fase faseAtual, List<SpriteMove> spm, Canvas canvas, Jerry j) {
        this.spm = spm;
        jk = j;
        faseAtual.animarExercicio(spm, canvas,j);
    }

    // método que é chamado na Thread de UI para atualizar a tela
    @Override
    public void onDraw(Canvas canvas) {

        canvas.drawColor(Color.WHITE);

        this.ffl.animarExercicio(this.spm, canvas,jk);

        for (SpriteMove s : this.spm)
            s.getSp().onDraw(canvas);
    }
}
