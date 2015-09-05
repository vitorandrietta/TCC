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
public class FonemaEntity extends BaseModel {
    @Column(name = BaseColumns._ID)

    @PrimaryKey(autoincrement = true)
    Long id ;

    @Column
    String representacaoDicionario;

    @Column
    String formaFalada;

    public String getRepresentacaoDicionario() {
        return representacaoDicionario;
    }

    public String getFormaFalada() {
        return formaFalada;
    }

    public FonemaEntity(String formaFalada, String representacaoDicionario) {
        this.formaFalada = formaFalada;
        this.representacaoDicionario = representacaoDicionario;

    }

    public Long getId() {
        return id;

    }


}

