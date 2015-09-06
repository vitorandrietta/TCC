package game;

import android.graphics.Canvas;

/**
 * Created by root on 05/09/15.
 */
public class GameLoopThread extends Thread {

    private static final long FPS = 10;
    private CanvasView screen;
    private boolean running =  false;

    public GameLoopThread(CanvasView c) {
        this.screen = c;
    }

    public void setRunning(boolean run) {
        this.running = run;
    }

    @Override
    public void run() {
        long ticksPS = 1000 / FPS;
        long startTime;
        long sleepTime;

        while (running) {
            Canvas c = null;
            startTime = System.currentTimeMillis();

            try {
                c = screen.getHolder().lockCanvas();
                synchronized (screen.getHolder()) {
                    screen.onDraw(c);
                }

            } finally {
                if (c != null)
                    screen.getHolder().unlockCanvasAndPost(c);
            }

            sleepTime = ticksPS-(System.currentTimeMillis() - startTime);

            try {
                if (sleepTime > 0)
                    sleep(sleepTime);
                else
                    sleep(10);

            } catch (Exception e) {}

        }
    }
}
