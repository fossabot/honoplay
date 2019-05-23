import React from 'react';
import terasuProxy from '@omegabigdata/terasu-api-proxy';
import { questions, newQuestion } from '../../helpers/TerasuKey';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Table from '../../components/Table/TableComponent';

class Sorular extends React.Component {

constructor(props) {
    super(props);
    this.state= {
        sorularColumns: ['Sorular', 'Soru Tipi', 'Kategori', ' '],
        sorularData: [{  'id': 0,
                         'Sorular':'2025 Arenadaki en iyi tasarımcı kimdir?',
                         'Soru Tipi': 'Görselli',
                         'Kategori': 'Genel Kültür',
                        },
                        { 'id': 1,
                          'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                          'Soru Tipi': 'Düz Metin',
                          'Kategori': 'Tarih',
                        },
                        { 'id': 2,
                          'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                          'Soru Tipi': 'Düz Metin',
                          'Kategori': 'Coğrafya',
                        },
                        { 'id': 3,
                          'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                          'Soru Tipi': 'Düz Metin',
                          'Kategori': 'Edebiyat',
                        },
                        { 'id': 4,
                          'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                          'Soru Tipi': 'Düz Metin',
                          'Kategori': 'Matematik',
                        },
                        { 'id': 5,
                          'Sorular':'Türkiyede en çok kıskanılan takımın Fenerbahçe olmasının asıl sebebi nedir?',
                          'Soru Tipi': 'Düz Metin',
                          'Kategori': 'Biyoloji',
                        }]
    };
}

render() {
  const { classes } = this.props;
  
    return (
        <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={6} sm={10}>
            <Typography 
              pageHeader={terasuProxy.translate(questions)}
            />
          </Grid>
          <Grid item xs={6} sm={2}> 
            <Button 
              buttonColor="secondary" 
              buttonIcon="plus"
              buttonName={terasuProxy.translate(newQuestion)}
            />   
          </Grid>
          <Grid item xs={12} sm={12}>  
            <Table 
              columns={this.state.sorularColumns}        
              data={this.state.sorularData}
            />
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(Sorular);