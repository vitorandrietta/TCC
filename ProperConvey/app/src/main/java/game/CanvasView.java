package game;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Rect;
import android.util.AttributeSet;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;

import java.util.ArrayList;
import java.util.List;

import properconvey.com.br.properconvey.R;
import properconvey.com.br.properconvey.fases.FaseFlorestaLaranja;

/**
 * Created by root on 05/09/15.
 */
public class CanvasView extends SurfaceView {

    private Context context;
    private Sprite sp;
    private Background background;

    private SurfaceHolder holder;
    private GameLoopThread gameThread;

    private List<Coordenada> c;
    private FaseFlorestaLaranja ffl;

    public CanvasView(Context context) {
        super(context);
        this.context = context;

        this.gameThread = new GameLoopThread(this);
        this.holder = getHolder();
        this.holder.addCallback(new SurfaceHolder.Callback(){
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
                    } catch (InterruptedException e) {}
                }
            }

            @Override
            public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) { }
        });


        Bitmap bmp = BitmapFactory.decodeResource(getResources(), R.drawable.outrojerry);
        this.sp = new Sprite(bmp,this);

        this.c = new ArrayList<Coordenada>();
        this.c.add(new Coordenada(100,100));
        this.ffl = new FaseFlorestaLaranja(this, null,BitmapFactory.decodeResource(getResources(), R.drawable.floresta));
        ffl.animarExercicio(c, this.sp);
    }

    public void setBackground(Bitmap bmp) {
        this.background = new Background(bmp, this);
    }

    @Override
    public void onDraw(Canvas canvas) {
        canvas.drawColor(Color.WHITE);

        this.background.onDraw(canvas);
        this.sp.onDraw(canvas);
    }
}
