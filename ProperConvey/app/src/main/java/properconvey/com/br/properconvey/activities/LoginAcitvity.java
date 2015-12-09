package properconvey.com.br.properconvey.activities;


import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import properconvey.com.br.properconvey.R;

public class LoginAcitvity extends AppCompatActivity {

    public final static String INICIO_GAME = "6868";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_first_acitvity);
        getSupportActionBar().hide();
        Button logar = (Button) findViewById(R.id.btnLogar);
        logar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                EditText senha = (EditText) findViewById(R.id.passwordEdit);
                EditText nome = (EditText) findViewById(R.id.nomeEdit);
                if(!senha.getText().toString().trim().isEmpty() && !nome.getText().toString().isEmpty()) {
                    Intent game = new Intent(LoginAcitvity.this, MainActivity.class);
                    game.putExtra(INICIO_GAME, nome.getText().toString());
                    startActivity(game);
                }
            }
        });

    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_first_acitvity, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
         int id = item.getItemId();

        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
