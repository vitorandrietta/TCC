package properconvey.com.br.properconvey.database.entity;

import android.provider.BaseColumns;

import com.raizlabs.android.dbflow.annotation.Column;
import com.raizlabs.android.dbflow.annotation.PrimaryKey;
import com.raizlabs.android.dbflow.annotation.Table;
import com.raizlabs.android.dbflow.structure.BaseModel;

import properconvey.com.br.properconvey.database.core.ProperConveyDatabase;

/**
 * Created by root on 05/09/15.
 */

@Table(databaseName = ProperConveyDatabase.NAME)
public class PacienteEntity extends BaseModel {

        @Column(name = BaseColumns._ID, length = 11)
        @PrimaryKey (autoincrement = true)
        Long id ;

        @Column
        String nome;

        @Column
        String cep;

        @Column
        Long idDeficiencia; //fk

        @Column
        String username;

        @Column
        String senha;

        @Column
        String rg;

        @Column
        String crmMedico; //fk

        @Column
        String cpf;

}
