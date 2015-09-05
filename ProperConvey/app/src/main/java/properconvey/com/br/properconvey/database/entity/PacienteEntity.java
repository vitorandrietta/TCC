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

        @Column(name = BaseColumns._ID)
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


    public PacienteEntity(String nome, String cep, Long idDeficiencia,
                          String username,String senha,String rg,String crmMedico,String cpf){
    this.cep = cep;
    this.cpf = cpf;
    this.crmMedico = crmMedico;
    this.idDeficiencia = idDeficiencia;
    this.nome = nome;
    this.rg = rg;
    this.senha = senha;
    this.username = username;


    }

    public Long getId() {
        return id;
    }

    public String getCpf() {
        return cpf;
    }

    public String getCrmMedico() {
        return crmMedico;
    }

    public String getSenha() {
        return senha;
    }

    public String getUsername() {
        return username;
    }

    public String getCep() {
        return cep;
    }

    public String getNome() {
        return nome;
    }

    public Long getIdDeficiencia() {
        return idDeficiencia;
    }

    public String getRg() {
        return rg;
    }

}
