package properconvey.com.br.properconvey.activities;


import static android.widget.Toast.makeText;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.util.ArrayList;
import java.util.List;

import android.content.ContentResolver;
import android.content.Intent;
import android.content.IntentSender;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.service.carrier.CarrierMessagingService;
import android.speech.RecognitionListener;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.drive.Drive;
import com.google.android.gms.drive.DriveApi;
import com.google.android.gms.drive.DriveContents;
import com.google.android.gms.drive.DriveFolder;
import com.google.android.gms.drive.MetadataChangeSet;

import properconvey.com.br.properconvey.R;
import properconvey.com.br.properconvey.faseFloresta.FaseFlorestaLaranja;
import properconvey.com.br.properconvey.game.CanvasView;
import properconvey.com.br.properconvey.game.Coordenada;
import properconvey.com.br.properconvey.game.SpriteMove;

//PocketSphinxActivity
public class LoginActivity extends AppCompatActivity implements
        RecognitionListener,GoogleApiClient.OnConnectionFailedListener {

    /* Named searches allow to quickly reconfigure the decoder */


    private SpeechRecognizer fala = null;
    private Intent recognizerIntent;
    private String LOG_TAG = "VoiceRecognitionActivity";
    private  String KEYPHRASE1 = "laranja";
    private MediaPlayer player;
    private int errorCount =0;
    private CanvasView canvasView;
    private boolean state = false;
    private GoogleApiClient googleApiClient;
    private final static  int RESOLVE_CONNECTION_REQUEST_CODE=6789;

    @Override
    public void onCreate(Bundle state) {

        this.googleApiClient = new GoogleApiClient.Builder(this)
                .addApi(Drive.API)
                .addScope(Drive.SCOPE_FILE)
                .addOnConnectionFailedListener(this)
                .build();

        super.onCreate(state);
        setContentView(R.layout.activity_login);
        this.canvasView = new CanvasView(this);
        setContentView(canvasView);
        getSupportActionBar().hide();

        fala = SpeechRecognizer.createSpeechRecognizer(this);
        fala.setRecognitionListener(this);
        recognizerIntent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
        recognizerIntent.putExtra("android.speech.extra.GET_AUDIO_FORMAT", "audio/AMR");
        recognizerIntent.putExtra("android.speech.extra.GET_AUDIO", true);
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_PREFERENCE, "pt");
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE,
                this.getPackageName());
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL,
                RecognizerIntent.LANGUAGE_MODEL_WEB_SEARCH);


        recognizerIntent.putExtra(RecognizerIntent.EXTRA_MAX_RESULTS, 1);

        player=MediaPlayer.create(this,R.raw.intro);

        player.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
            @Override
            public void onCompletion(MediaPlayer mp) {
                new AsyncTask<Void, Void, Void>(){
                    @Override
                    protected Void doInBackground(Void... params) {
                        if(!LoginActivity.this.state) {
                            fala.startListening(recognizerIntent);
                        }
                            return null;
                    }
                }.doInBackground();
            }
        });

        player.start(); //audio falando laranja ao invés de orange
;

    }


    @Override
    public void onPartialResults(Bundle partialResults) {
        Log.i(LOG_TAG, "onPartialResults");
    }

    @Override
    public void onEvent(int eventType, Bundle params) {
        Log.i(LOG_TAG, "onEvent");
    }


    @Override
    public void onReadyForSpeech(Bundle params) {
        Log.i(LOG_TAG, "onReadyForSpeech");
    }

    @Override
    public void onBeginningOfSpeech() {
        Log.i(LOG_TAG, "onBeginningOfSpeech");
    }

    @Override
    public void onRmsChanged(float rmsdB) {
        Log.i(LOG_TAG, "onRmsChanged: " + rmsdB);
    }

    @Override
    public void onBufferReceived(byte[] buffer) {
        Log.i(LOG_TAG, "onBufferReceived: " + buffer);
    }

    @Override
    public void onEndOfSpeech() {
        Log.i(LOG_TAG, "onEndOfSpeech");
    }

    @Override
    public void onError(int error) {
        //...
    }


    @Override
    public void onResults(Bundle results) {

        ArrayList<String> matches = results
                .getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
        ContentResolver contentResolver = getContentResolver();


        if (matches.get(0).equalsIgnoreCase(KEYPHRASE1)) {

            ArrayList<String> extraMatches = results.getStringArrayList(RecognizerIntent.EXTRA_RESULTS);
            Uri audioUrl = recognizerIntent.getData();
            InputStream filestream=null;
            try {
                filestream = contentResolver.openInputStream(audioUrl);
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            }

            final BufferedReader reader = new BufferedReader(new InputStreamReader(filestream));

            ResultCallback <DriveApi.DriveContentsResult> driveContentsCallback;


              driveContentsCallback = new
                    ResultCallback<DriveApi.DriveContentsResult>() {
                        @Override
                        public void onResult(DriveApi.DriveContentsResult result) {
                            if (!result.getStatus().isSuccess()) {

                                return;
                            }
                            final DriveContents driveContents = result.getDriveContents();

                            // Perform I/O off the UI thread.
                            new Thread() {
                                @Override
                                public void run() {
                                    // write content to DriveContents
                                    OutputStream outputStream = driveContents.getOutputStream();
                                    Writer writer = new OutputStreamWriter(outputStream);
                                    try {
                                        while(reader.ready()) {
                                            writer.write(reader.read());
                                        }
                                            writer.flush();
                                            writer.close();

                                    } catch (IOException e) {

                                    }

                                    MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                                            .setTitle("Exer"+(Math.floor(Math.random()*1000000000))+"-"+errorCount)
                                            .setMimeType("text/plain")
                                            .setStarred(true).build();


                                    // create a file on root folder
                                    Drive.DriveApi.getRootFolder(googleApiClient)
                                            .createFile(googleApiClient, changeSet, driveContents);

                                }
                            }.start();
                        }
                    };

            Drive.DriveApi.newDriveContents(googleApiClient).setResultCallback(driveContentsCallback);


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
            return;
        }

        else{
        errorCount++;
        MediaPlayer mpr =    MediaPlayer.create(this, R.raw.diga_laranja);
        mpr.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {


            @Override
            public void onCompletion(MediaPlayer mp) {
                new AsyncTask<Void, Void, Void>(){
                    @Override
                    protected Void doInBackground(Void... params) {
                        if(!LoginActivity.this.state) {
                            fala.startListening(recognizerIntent);
                        }
                        return  null;
                    }
                }.doInBackground();
            }
        });


            mpr.start();


        }



    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        switch (requestCode) {
            case RESOLVE_CONNECTION_REQUEST_CODE:
                if (resultCode == RESULT_OK) {
                    this.googleApiClient.connect();
                }
                break;
        }

    }

    @Override
    protected void onStart() {
        super.onStart();
        this.googleApiClient.connect();
    }

    @Override
    public void onConnectionFailed(ConnectionResult connectionResult) {
        if (connectionResult.hasResolution()) {
            try {
                connectionResult.startResolutionForResult(this, RESOLVE_CONNECTION_REQUEST_CODE);
            } catch (IntentSender.SendIntentException e) {
                // Unable to resolve, message user appropriately
            }
        } else {
            GooglePlayServicesUtil.getErrorDialog(connectionResult.getErrorCode(), this, 0).show();
        }

    }
}












