package database.entity;

import android.provider.BaseColumns;

import com.raizlabs.android.dbflow.annotation.Column;
import com.raizlabs.android.dbflow.annotation.ForeignKey;
import com.raizlabs.android.dbflow.annotation.PrimaryKey;
import com.raizlabs.android.dbflow.annotation.Table;
import com.raizlabs.android.dbflow.structure.BaseModel;

import database.core.ProperConveyDatabase;

/**
 * Created by root on 05/09/15.
 */
@Table(databaseName = ProperConveyDatabase.NAME)
public class PacienteEntity extends BaseModel {

        @Column(name = BaseColumns._ID)
        @PrimaryKey(autoincrement = true)
        String cpf;

        @Column
        String nome;

        @Column
        String cep;

        @Column
        Long idDeficiencia;

        @Column
        String username;

        @Column
        String senha;


}
