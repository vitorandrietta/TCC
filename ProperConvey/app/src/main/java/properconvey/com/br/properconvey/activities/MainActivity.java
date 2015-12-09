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
import android.speech.RecognitionListener;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.drive.Drive;
import com.google.android.gms.drive.DriveApi;
import com.google.android.gms.drive.DriveContents;
import com.google.android.gms.drive.MetadataChangeSet;

import properconvey.com.br.properconvey.R;
import properconvey.com.br.properconvey.faseFloresta.FaseFlorestaLaranja;
import properconvey.com.br.properconvey.game.CanvasView;
import properconvey.com.br.properconvey.game.Coordenada;
import properconvey.com.br.properconvey.game.SpriteMove;

public class MainActivity extends AppCompatActivity implements
        RecognitionListener,GoogleApiClient.OnConnectionFailedListener {

    private SpeechRecognizer fala = null;
    private Intent recognizerIntent;
    public final static String EXERCISE_NAME ="Exercise1";
    private String LOG_TAG = "VoiceRecognitionActivity";
    private  String KEYPHRASE1 = "laranja";
    private MediaPlayer player;
    private int tentativas =1;
    private CanvasView canvasView;
    private boolean state = false;
    private GoogleApiClient googleApiClient;
    private final static  int RESOLVE_CONNECTION_REQUEST_CODE=6789;
    private final static int SPEAK_REQUEST_CODE = 8967;
    private List<Long> timeIntervals;
    private long intialTime;
    private String nome;

    @Override
    public void onCreate(Bundle state) {


        super.onCreate(state);
        this.canvasView = new CanvasView(this);
        Intent intent = getIntent();
        this.nome = intent.getStringExtra(LoginAcitvity.INICIO_GAME);
        setContentView(canvasView);
        getSupportActionBar().hide();
        this.timeIntervals = new ArrayList<>();
        this.googleApiClient = new GoogleApiClient.Builder(this)
                .addApi(Drive.API)
                .addScope(Drive.SCOPE_FILE)
                .addOnConnectionFailedListener(this)
                .build();

        fala = SpeechRecognizer.createSpeechRecognizer(this);
        fala.setRecognitionListener(this);

        recognizerIntent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_PREFERENCE, "pt");
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE,
                this.getPackageName());
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL,
                RecognizerIntent.LANGUAGE_MODEL_WEB_SEARCH);

        //OW
        recognizerIntent.putExtra("android.speech.extra.GET_AUDIO_FORMAT", "audio/AMR");
        recognizerIntent.putExtra("android.speech.extra.GET_AUDIO", true);
        //OW

        recognizerIntent.putExtra(RecognizerIntent.EXTRA_MAX_RESULTS, 1);

        player=MediaPlayer.create(this,R.raw.intro);

        player.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
            @Override
            public void onCompletion(MediaPlayer mp) {
                new AsyncTask<Void, Void, Void>(){
                    @Override
                    protected Void doInBackground(Void... params) {
                        if(!MainActivity.this.state) {

                            startActivityForResult(recognizerIntent,SPEAK_REQUEST_CODE);
                            intialTime = System.currentTimeMillis();

                        }
                            return null;
                    }
                }.doInBackground();
            }
        });

        player.start();
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



    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        switch (requestCode) {
            case RESOLVE_CONNECTION_REQUEST_CODE:
                if (resultCode == RESULT_OK) {
                    this.googleApiClient.connect();
                }

            case SPEAK_REQUEST_CODE:{
                Bundle bundle = data.getExtras();
                ArrayList<String> matches = bundle.getStringArrayList(RecognizerIntent.EXTRA_RESULTS);

                long interval = System.currentTimeMillis()-this.intialTime;
                this.timeIntervals.add(interval);

                if(matches.get(0).equalsIgnoreCase(KEYPHRASE1)){
                    Uri audioUri = data.getData();
                    ContentResolver contentResolver = getContentResolver();
                    InputStream filestream = null;
                    try {
                         filestream = contentResolver.openInputStream(audioUri);
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


                                    new Thread() {
                                        @Override
                                        public void run() {

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

                                            String tentativeTimes="-";
                                            for(Long l: timeIntervals){
                                                tentativeTimes+=String.valueOf(l).concat("-");

                                            }

                                            tentativeTimes = tentativeTimes.substring(0,tentativeTimes.lastIndexOf('-'));

                                            MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                                                    .setTitle("ppcn_Exer1_"+MainActivity.this.nome+"_"+(Math.floor(Math.random()*100)+1)+"-"+tentativas+tentativeTimes)
                                                    .setMimeType("text/plain")
                                                    .setStarred(true).build();
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
                    tentativas++;
                    MediaPlayer mpr =    MediaPlayer.create(this, R.raw.diga_laranja);
                    mpr.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {


                        @Override
                        public void onCompletion(MediaPlayer mp) {
                            new AsyncTask<Void, Void, Void>(){
                                @Override
                                protected Void doInBackground(Void... params) {
                                    if(!MainActivity.this.state) {
                                        intialTime = System.currentTimeMillis();
                                        recognizerIntent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
                                        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_PREFERENCE, "pt");
                                        recognizerIntent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE,
                                                MainActivity.this.getPackageName());
                                        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL,
                                                RecognizerIntent.LANGUAGE_MODEL_WEB_SEARCH);


                                        recognizerIntent.putExtra("android.speech.extra.GET_AUDIO_FORMAT", "audio/AMR");
                                        recognizerIntent.putExtra("android.speech.extra.GET_AUDIO", true);


                                        recognizerIntent.putExtra(RecognizerIntent.EXTRA_MAX_RESULTS, 1);
                                        startActivityForResult(recognizerIntent,SPEAK_REQUEST_CODE);
                                    }
                                    return  null;
                                }
                            }.doInBackground();
                        }
                    });

                    mpr.start();
                }
            }

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
                //Nao deu para resolver o erro apropriadamente D=
            }
        } else {
            GooglePlayServicesUtil.getErrorDialog(connectionResult.getErrorCode(), this, 0).show();
        }

    }
}












