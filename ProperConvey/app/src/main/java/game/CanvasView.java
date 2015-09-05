package game;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.util.AttributeSet;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;

import properconvey.com.br.properconvey.R;

/**
 * Created by root on 05/09/15.
 */
public class CanvasView extends SurfaceView {

    private Context context;
    private Bitmap jerry;
    private Sprite sp;

    private Canvas canvas;

    private SurfaceHolder holder;
    private GameLoopThread gameThread;

    public CanvasView(Context context, AttributeSet attrs) {
        super(context, attrs);
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

        this.jerry = BitmapFactory.decodeResource(getResources(), R.drawable.spritejerry);
        this.sp = new Sprite(this.jerry);
    }

    @Override
    public void onDraw(Canvas canvas) {
        //super.onDraw(canvas);
        this.canvas = canvas;
        this.canvas.drawColor(Color.WHITE);
        this.sp.onDraw(this.canvas);
    }

    public void start() {
        long com = System.currentTimeMillis();
        long fim = 0;

        while (true) {
            fim = System.currentTimeMillis();

            if (fim - com >= 300) {
                com = System.currentTimeMillis();
                sp.onDraw(this.canvas);
            }
        }
    }
}
