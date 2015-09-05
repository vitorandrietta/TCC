package game;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Color;
import android.view.View;

import properconvey.com.br.properconvey.R;

/**
 * Created by root on 05/09/15.
 */
public class CanvasView extends View {

    private Context context;
    private Bitmap jerry;

    public CanvasView(Context context) {
        super(context);

        this.context = context;
        this.jerry = BitmapFactory.decodeResource(getResources(), R.drawable.jerry);
    }

    @Override
    public void onDraw(Canvas canvas) {
        super.onDraw(canvas);

        // draw jerry on canvas

        canvas.drawColor(Color.WHITE);

    }
}
