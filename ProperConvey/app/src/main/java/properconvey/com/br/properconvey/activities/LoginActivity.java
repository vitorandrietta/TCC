package properconvey.com.br.properconvey.activities;


import static android.widget.Toast.makeText;
import static edu.cmu.pocketsphinx.SpeechRecognizerSetup.defaultSetup;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import android.app.Activity;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.media.MediaPlayer;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.TextView;
import android.widget.Toast;
import edu.cmu.pocketsphinx.Assets;
import edu.cmu.pocketsphinx.Hypothesis;
import edu.cmu.pocketsphinx.RecognitionListener;
import edu.cmu.pocketsphinx.SpeechRecognizer;
import properconvey.com.br.properconvey.R;
import properconvey.com.br.properconvey.faseFloresta.FaseFlorestaLaranja;
import properconvey.com.br.properconvey.game.CanvasView;
import properconvey.com.br.properconvey.game.Coordenada;
import properconvey.com.br.properconvey.game.SpriteMove;

//PocketSphinxActivity
public class LoginActivity extends AppCompatActivity implements
        RecognitionListener {

    /* Named searches allow to quickly reconfigure the decoder */
    private static final String KWS_SEARCH = "wakeup";
    private MediaPlayer player;
    private static final String MENU_SEARCH = "menu";
    //lista de frases que mudam com o exercício;
    private static final String KEYPHRASE = "orange";
    private CanvasView canvasView;
    private boolean state = false;

    private SpeechRecognizer recognizer;
    @Override
    public void onCreate(Bundle state) {
        super.onCreate(state);
        setContentView(R.layout.activity_login);
        this.canvasView = new CanvasView(this);
        setContentView(canvasView);

        getSupportActionBar().hide();


            new AsyncTask<Void, Void, Exception>() {
            @Override
            protected Exception doInBackground(Void... params) {
                try {
                    Assets assets = new Assets(LoginActivity.this);
                    File assetDir = assets.syncAssets();
                    setupRecognizer(assetDir);
                } catch (IOException e) {
                    return e;
                }
                return null;
            }

            @Override
            protected void onPostExecute(Exception result) {
                if (result != null) {
                } else {
                    //KWS SEARCH
                }
            }
        }.execute();


        player=MediaPlayer.create(this,R.raw.intro);
        player.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
            @Override
            public void onCompletion(MediaPlayer mp) {
                switchSearch(KWS_SEARCH);
            }
        });

        player.start();

;

    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        recognizer.cancel();
        recognizer.shutdown();
    }


    @Override
    public void onPartialResult(Hypothesis hypothesis) {
        if (hypothesis == null)
            return;

        String text = hypothesis.getHypstr();
        if (text.equals(KEYPHRASE)) {


            List<SpriteMove> sprites = new ArrayList<SpriteMove>();
            List<Coordenada> coords1 = new ArrayList<Coordenada>();
            List<Coordenada> coords2 = new ArrayList<Coordenada>();


            coords1.add(new Coordenada(310, 230));
            coords2.add(new Coordenada(300, 250));
            sprites.add(new SpriteMove(coords1, canvasView.getJerry()));
            sprites.add(new SpriteMove(coords2, canvasView.getLaranja()));



            FaseFlorestaLaranja floresta = new FaseFlorestaLaranja(canvasView, BitmapFactory.
                    decodeResource(getResources(), R.drawable.floresta));
            canvasView.animarParteFase(floresta, sprites, new Canvas(), canvasView.getJerry());

            try {
                Thread.sleep(3500);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            if (!state){

                player = MediaPlayer.create(this,R.raw.congrats);
                player.start();
            }

            state = true;

            //switchSearch(MENU_SEARCH);
        }

    }


    @Override
    public void onResult(Hypothesis hypothesis) {
        //((TextView) findViewById(R.id.result_text)).setText("");
        if (hypothesis != null) {
            String text = hypothesis.getHypstr();
            makeText(getApplicationContext(), text, Toast.LENGTH_SHORT).show();
        }
    }

    @Override
    public void onBeginningOfSpeech() {
    }

    /**
     * We stop recognizer here to get a final result
     */
    @Override
    public void onEndOfSpeech() {
        if (!recognizer.getSearchName().equals(KWS_SEARCH))
            switchSearch(KWS_SEARCH);
    }

    private void switchSearch(String searchName) {
        recognizer.stop();
        if (searchName.equals(KWS_SEARCH))
            recognizer.startListening(searchName);
        else
         recognizer.startListening(searchName, 10000);


    }

    private void setupRecognizer(File assetsDir) throws IOException {


        recognizer = defaultSetup()
                .setAcousticModel(new File(assetsDir, "en-us-ptm"))
                .setDictionary(new File(assetsDir, "cmudict-en-us.dict"))
                .setRawLogDir(assetsDir)
                .setKeywordThreshold(1e-28f)
                .setBoolean("-allphone_ci", true)
                .getRecognizer();
        recognizer.addListener(this);

        recognizer.addKeyphraseSearch(KWS_SEARCH, KEYPHRASE);
        File menuGrammar = new File(assetsDir, "menu.gram");
        File digitsGrammar = new File(assetsDir, "digits.gram");

    }

    @Override
    public void onError(Exception error) {

    }

    @Override
    public void onTimeout() {
        switchSearch(KWS_SEARCH);
    }


}


