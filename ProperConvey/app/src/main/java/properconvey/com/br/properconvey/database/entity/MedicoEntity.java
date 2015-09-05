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
public class MedicoEntity extends BaseModel {
    @Column(name = BaseColumns._ID)
    @PrimaryKey(autoincrement = true)
    Long id ;

    @Column
    String nome;

    @Column
    String crm;

    @Column
    String senha; //desnecessario localmente ?

    @Column
    String cep;

    @Column
    Long idEspecialidade;

    @Column (length = 11)
    String cpf;

    @Column
    String rg;

    public Long getId() {
        return id;
    }

    public String getRg() {
        return rg;
    }

    public String getCpf() {
        return cpf;
    }

    public Long getIdEspecialidade() {
        return idEspecialidade;
    }

    public String getCep() {
        return cep;
    }

    public String getSenha() {
        return senha;
    }

    public String getCrm() {
        return crm;
    }

    public String getNome() {
        return nome;
    }

    public MedicoEntity(String crm, String nome, String senha, String rg, String cpf, String cep, Long idEspecialidade) {
        this.crm = crm;
        this.nome = nome;
        this.senha = senha;
        this.rg = rg;
        this.cpf = cpf;
        this.cep = cep;
        this.idEspecialidade = idEspecialidade;
    }
}


