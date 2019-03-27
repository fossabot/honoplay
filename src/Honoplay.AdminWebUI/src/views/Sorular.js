import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Typography} from '@material-ui/core';
import TableComponent from '../components/Table/TableComponent';
import ButtonComponent from '../components/Button/ButtonComponent';

const styles = theme => ({
    root: {
      flexGrow: 1,
    },
    Typography: {
        margin: theme.spacing.unit,
        color: '#e92428',
        fontWeight: 'bold',
    },
});

function Sorular(props) {
    const { classes } = props;
  
    const columns = ['Sorular', 'Soru Tipi', 'Kategori'];
    const data = [{ 'Sorular':'2025 Arenadaki en iyi tasarımcı kimdir?',
                    'Soru Tipi': 'Görselli',
                    'Kategori': 'Genel Kültür',
                  },
                  { 'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                    'Soru Tipi': 'Düz Metin',
                    'Kategori': 'Tarih',
                  },
                  { 'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                  'Soru Tipi': 'Düz Metin',
                  'Kategori': 'Tarih',
                  },
                  { 'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                  'Soru Tipi': 'Düz Metin',
                  'Kategori': 'Tarih',
                  },
                  { 'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                  'Soru Tipi': 'Düz Metin',
                  'Kategori': 'Tarih',
                  },
                  { 'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                  'Soru Tipi': 'Düz Metin',
                  'Kategori': 'Tarih',
                  }];
    
    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
            <Grid item sm={9}>
                <Typography variant="h5" className={classes.Typography} >
                Sorular
                </Typography>
            </Grid>
            <Grid item sm={3}>
                <ButtonComponent ButtonColor="primary" 
                                ButtonName="Yeni Soru Ekle"/>          
            </Grid>
            <Grid item xs={12}>
                <Typography variant="body2" gutterBottom>
                    Bu soru setinde bulunmasını istediğiniz soruları aşağıdaki tablodan görebilir, 
                    düzenleyebilir ve yeni soru ekleyebilirsiniz.
                </Typography>
            </Grid>
            <Grid item xs={12}>
                <TableComponent columns={columns} 
                                data={data}
                                RowNumber="true"
                />
            </Grid>
            <Grid item xs={12} sm={3}>
                <ButtonComponent ButtonColor="primary" 
                                ButtonIcon="file-excel" 
                                ButtonName="Excel Formatında İndir"/>  
            </Grid>           
            <Grid item xs={12} sm={3}>  
                <ButtonComponent ButtonColor="secondary" 
                                 ButtonIcon="file-excel" 
                                 ButtonName="Excel'den Aktar"
                />  
            </Grid>
            <Grid item xs={2}></Grid>
        </Grid>
      </div>
    );
  }

  
Sorular.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(Sorular);