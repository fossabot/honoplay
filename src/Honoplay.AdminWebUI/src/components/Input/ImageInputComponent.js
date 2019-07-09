import React from 'react';
import { Button, InputBase, Grid, InputLabel } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Style } from './Style';
import { get } from 'http';

class ImageInputComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      filename: '',
      byteArray: null
    }
    this.handleImageName = this.handleImageName.bind(this);
    this.getBase64 = this.getBase64.bind(this);
  }
  handleImageName = (e) => {
    this.setState({
      filename: e.target.value.replace("C:\\fakepath\\", ""),
    });
    this.getBase64(e.target.files[0]);
  }

  getBase64 = (file) => {
    if (!file) {
      return;
    }
    const reader = new FileReader();
    reader.readAsArrayBuffer(file);

    reader.onload = (e) => {
      const arrayBuffer = e.target.result,
        array = Array.from(new Uint8Array(arrayBuffer));
      this.props.selectedImage(array)
      this.setState({ byteArray: array })
    }
  };


  render() {
    const { classes, labelName } = this.props;


    return (
      <div className={classes.inputRoot}>
        <Grid container spacing={24}>
          {(labelName &&
            <Grid item xs={12} sm={3}
              className={classes.labelCenter}>
              <InputLabel className={classes.bootstrapFormLabel}>
                {labelName}
              </InputLabel>
            </Grid>
          )}
          <Grid item xs={8} sm={5}>
            <InputBase 
              
              classes={{
                root: classes.bootstrapRoot,
                input: classes.bootstrapInput,
              }}
              value={this.state.filename}
              fullWidth
            />
          </Grid>
          <Grid item xs={4} sm={4}>
            <input accept="image/*"
              className={classes.fileInput}
              id="button-file"
              multiple
              type="file"
              onChange={this.handleImageName}

            />
            <label htmlFor="button-file" >
              <Button variant="outlined"
                component="span"
                className={classes.fileInputButton}>
                {translate('Browse')}
              </Button>
            </label>
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(Style)(ImageInputComponent);
