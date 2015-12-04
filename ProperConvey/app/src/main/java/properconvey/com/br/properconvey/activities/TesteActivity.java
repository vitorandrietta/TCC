package properconvey.com.br.properconvey.activities;

import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentSender;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.os.Bundle;
import android.speech.RecognitionListener;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;

import com.google.android.gms.auth.GoogleAuthException;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.Drive;
import com.google.android.gms.drive.DriveApi;
import com.google.android.gms.drive.DriveContents;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.MetadataChangeSet;
import com.google.android.gms.drive.events.CompletionEvent;

import java.io.*;
import java.util.ArrayList;

import properconvey.com.br.properconvey.R;

public class TesteActivity extends AppCompatActivity implements RecognitionListener, GoogleApiClient.ConnectionCallbacks , GoogleApiClient.OnConnectionFailedListener{
    private InputStream filleStream;
    private SpeechRecognizer fala = null;
    private Intent recognizerIntent;
    private String LOG_TAG = "VoiceRecognitionActivity";
    private GoogleApiClient mGoogleApiClient;
    private final static int RESOLVE_CONNECTION_REQUEST_CODE =562;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_teste);

        fala = SpeechRecognizer.createSpeechRecognizer(this);
        fala.setRecognitionListener(this);
        recognizerIntent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_PREFERENCE,"pt");

        recognizerIntent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE,
        this.getPackageName());
        recognizerIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL,
        RecognizerIntent.LANGUAGE_MODEL_WEB_SEARCH);
        //recognizerIntent.putExtra("android.speech.extra.GET_AUDIO_FORMAT", "audio/AMR");
        //recognizerIntent.putExtra("android.speech.extra.GET_AUDIO", true);

        /*mGoogleApiClient = new GoogleApiClient.Builder(this)
                .addApi(Drive.API)
                .addScope(Drive.SCOPE_FILE)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .build();*/

        recognizerIntent.putExtra(RecognizerIntent.EXTRA_MAX_RESULTS, 3);
        fala.startListening(recognizerIntent);
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
        Uri audioUri = recognizerIntent.getData();
        ContentResolver contentResolver = getContentResolver();
        filleStream = null;
        try {
            filleStream = contentResolver.openInputStream(audioUri);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }

        //Drive.DriveApi.newDriveContents(mGoogleApiClient).;

      /*  if(this.isNetworkAvailable()){





            ResultCallback<DriveApi.DriveContentsResult> contentsCallback = new
                    ResultCallback<DriveApi.DriveContentsResult>() {


                        @Override
                        public void onResult(DriveApi.DriveContentsResult result) {
                            if (!result.getStatus().isSuccess()) {
                                // Handle error
                                return;
                            }


                            DriveContents contents = result.getDriveContents();
                            OutputStream oos = contents.getOutputStream();


                            ByteArrayOutputStream bos = new ByteArrayOutputStream();
                            byte[] b = new byte[1024];
                            while (true) {

                                int bytesRead = 0;
                                try {
                                    bytesRead = filleStream.read(b);
                                } catch (IOException e) {
                                    e.printStackTrace();
                                }

                                if(bytesRead == -1){

                                    break;

                                }

                                try {
                                    oos.write(b);
                                } catch (IOException e) {
                                    e.printStackTrace();
                                }

                                try {
                                    oos.flush();
                                } catch (IOException e) {
                                    e.printStackTrace();
                                }


                            }


                            try {
                                oos.close();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }


                            MetadataChangeSet changeSet = new MetadataChangeSet.Builder()
                                    .setTitle("file"+Math.floor(Math.random()*1000))
                                    .setMimeType("text/plain").build();



                            Drive.DriveApi.getRootFolder(mGoogleApiClient)
                                    .createFile(mGoogleApiClient, changeSet, contents);

                        }
                    };

            Drive.DriveApi.newDriveContents(mGoogleApiClient)
                    .setResultCallback(contentsCallback);


        }*/

        //System.out.print("lalala");
        fala.startListening(recognizerIntent);

    }


    /*private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }*/
    @Override
    public void onPartialResults(Bundle partialResults) {
        Log.i(LOG_TAG, "onPartialResults");
    }

    @Override
    public void onEvent(int eventType, Bundle params) {
        Log.i(LOG_TAG, "onEvent");
    }

    @Override
    protected void onStart() {
        super.onStart();
        this.mGoogleApiClient.connect();
    }

    @Override
    public void onConnected(Bundle bundle) {

    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onConnectionFailed(ConnectionResult connectionResult) {
        if (connectionResult.hasResolution()) {
            try {
                connectionResult.startResolutionForResult(this, RESOLVE_CONNECTION_REQUEST_CODE);//constante
            } catch (IntentSender.SendIntentException e) {
                // Unable to resolve, message user appropriately
            }
        } else {
            GooglePlayServicesUtil.getErrorDialog(connectionResult.getErrorCode(), this, 0).show();
        }

    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        switch (requestCode) {
            case RESOLVE_CONNECTION_REQUEST_CODE:
                if (resultCode == RESULT_OK) {
                    mGoogleApiClient.connect();
                }
                break;
        }

    }
}
