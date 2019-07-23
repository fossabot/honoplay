import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Table from '../../components/Table/TableComponent';

class Questions extends React.Component {

constructor(props) {
    super(props);
    this.state= {
        questionsColumns:  [
          { title: "Sorular", field: "Sorular" },
          { title: "Soru Tipi", field: "Soru Tipi" },
          { title: "Kategori", field: "Kategori" },
        ],
        questionsData: [{  'id': 0,
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
handleClick = () => {
  this.props.history.push("/home/addquestion");
}
render() {
  const { classes } = this.props;
  
    return (
        <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={6} sm={10}>
            <Typography 
              pageHeader={translate('Questions')}
            />
          </Grid>
          <Grid item xs={6} sm={2}> 
            <Button 
              onClick= {this.handleClick}
              buttonColor="secondary" 
              buttonIcon="plus"
              buttonName={translate('NewQuestion')}
            />   
          </Grid>
          <Grid item xs={12} sm={12}>  
            <Table 
              columns={this.state.questionsColumns}        
              data={this.state.questionsData}
            />
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(Questions);